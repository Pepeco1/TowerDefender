﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public bool GamePaused
    {
        get => _gamePaused;

        set
        {
            _gamePaused = value;
            Time.timeScale = _gamePaused ? 0 : 1;
        }
    }

    [SerializeField] public UnityEvent onLevelEnded = null;

    private bool _gamePaused;

    // WaveSpawner and EnemyManager
    private bool FinishedSpawningEnemies = false;

    // Scene related
    private string _currentScene = string.Empty;
    private List<AsyncOperation> _loadingOperations = new List<AsyncOperation>();

    // Member variables
    private EnemyManager enemyManager = null;

    #region Unity Fuctions

    private void Awake()
    {
        enemyManager = EnemyManager.Instance;
    }

    private void Update()
    {
        if (FinishedSpawningEnemies && !enemyManager.HasEnemiesAlive())
        {

            FinishedSpawningEnemies = false;

            StartCoroutine(TerminateLevel());

            
        }
    }

    private void OnEnable()
    {
        RegisterForEvents();
    }

    private void OnDisable()
    {
        UnregisterForEvents();
    }

    #endregion

    #region Public Functions


    public AsyncOperation LoadSceneOnBackground(string sceneName)
    {
        return LoadScene(sceneName);
    }

    public void LoadSceneWithLoadingScreen(string sceneName)
    {
        var ao = LoadScenePausingGame(sceneName);
        UIManager.Instance.LoadingScreen(ao);
    }

    public void UnloadScene(string sceneName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(sceneName);

        if (ao != null)
        {
            ao.completed += OnUnloadSceneComplete;
            _loadingOperations.Add(ao);
        }
        else
        {
            Debug.LogError("[GameManager] Unable to unload scene " + sceneName);
        }
    }

    #region Delegators Subscription
    public void SubscribeToWaveSpanwer(WaveSpawner waveSpawner)
    {
        waveSpawner.OnFinishedSpawnning += WaveSpawner_OnFinishedSpawnning;
    }

    public void UnsubscribeToWaveSpawner(WaveSpawner waveSpawner)
    {
        waveSpawner.OnFinishedSpawnning -= WaveSpawner_OnFinishedSpawnning;
    }
    #endregion

    #endregion

    #region Private Functions

    private IEnumerator TerminateLevel()
    {

        yield return new WaitForSeconds(3f);

        Debug.Log("[GameManager] TODO- Load next game level");
        UnloadScene(SceneManager.GetActiveScene().name);
        onLevelEnded?.Invoke();

    }

    private void OnLoadSceneComplete(AsyncOperation ao)
    {
        if (_loadingOperations.Contains(ao))
        {
            _loadingOperations.Remove(ao);
        }

        ao.completed -= OnLoadSceneComplete;
    }

    private void OnUnloadSceneComplete(AsyncOperation ao)
    {
        Debug.Log("UnloadComplete");
    }

    private AsyncOperation LoadScene(string sceneName)
    {
        int Index = SceneManager.sceneCount;
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        if (ao != null)
        {
            _currentScene = sceneName;
            ao.completed += OnLoadSceneComplete;
            ao.completed += SetActiveScene;
            _loadingOperations.Add(ao);
        }
        else
        {
            Debug.LogError("[GameManager] Unable to load scene " + sceneName);
        }

        return ao;


        void SetActiveScene(AsyncOperation asyncOp)
        {
            var myScene = SceneManager.GetSceneAt(Index);
            SceneManager.SetActiveScene(myScene);

            asyncOp.completed -= SetActiveScene;
        }


    }

    private AsyncOperation LoadScenePausingGame(string sceneName)
    {
        GamePaused = true;
        return LoadScene(sceneName);
    }

    private void UIManager_OnLoadingScreenClose()
    {
        GamePaused = false;
    }

    private void UIManager_OnLoadingScreenOpen()
    {
        GamePaused = true;
    }

    private void RegisterForEvents()
    {
        UIManager.Instance.onLoadingScreenClose += UIManager_OnLoadingScreenClose;
        UIManager.Instance.onLoadingScreenOpen += UIManager_OnLoadingScreenOpen;
    }

    private void UnregisterForEvents()
    {
        UIManager.Instance.onLoadingScreenClose -= UIManager_OnLoadingScreenClose;
        UIManager.Instance.onLoadingScreenOpen -= UIManager_OnLoadingScreenOpen;
    }

    private void WaveSpawner_OnFinishedSpawnning()
    {
        FinishedSpawningEnemies = true;
    }
    #endregion
}
