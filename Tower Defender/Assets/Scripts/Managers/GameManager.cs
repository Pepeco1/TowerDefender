using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private string firstScene = string.Empty;

    private string _currentScene = string.Empty;
    private List<AsyncOperation> _loadingOperations = new List<AsyncOperation>();

    private bool _gamePaused;
    public bool GamePaused
    {
        get
        {
            return _gamePaused;
        }

        set
        {
            _gamePaused = value;
            Time.timeScale = _gamePaused ? 0 : 1;
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

    private void Start()
    {

        if(firstScene != string.Empty)
        {
            LoadSceneWithLoadingScreen(firstScene);
        }

    }


    private void OnLoadSceneComplete(AsyncOperation ao)
    {
        if (_loadingOperations.Contains(ao))
        {
            _loadingOperations.Remove(ao);
        }
    }

    private void OnUnloadSceneComplete(AsyncOperation ao)
    {
        Debug.Log("UnloadComplete");
    }

    private AsyncOperation LoadScene(string sceneName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        if (ao != null)
        {
            _currentScene = sceneName;
            ao.completed += OnLoadSceneComplete;
            _loadingOperations.Add(ao);
        }
        else
        {
            Debug.LogError("[GameManager] Unable to load scene " + sceneName);
        }

        return ao;
    }


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
}
