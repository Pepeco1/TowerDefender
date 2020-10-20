using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveSpawner : MonoBehaviour
{

    //Properties
    
    public bool FinishedSpawns => !HasNextWave() && !HasSequenceRunning();

    private Wave currentWave;
    private int currentWaveIndex = 0;

    [SerializeField] private float timeToNextWave = 3f;
    private float countDown = 0f;

    private int numRunningSequences = 0;
    [SerializeField] private List<Wave> levelWaves = new List<Wave>();

    // Member Variables
    [SerializeField] private Transform spawnTransform = null;
    [SerializeField] private Transform endTransform = null;

    // Delegators
    public UnityAction OnFinishedSpawnning = null;

    #region Unity Functions

    private void Awake()
    {

         ActivateWaves();


    }

    private void Update()
    {

        if (HasSequenceRunning())
        {
            return;
        }

        countDown -= Time.deltaTime;

        if (countDown <= 0)
        {
            if (HasNextWave())
            {
                StartCoroutine(SpawnWave(levelWaves[currentWaveIndex]));
                currentWaveIndex++;
            }
            else
            {
                OnFinishedSpawnning?.Invoke();
            }
        }

    }

    private void OnEnable()
    {
        GameManager.Instance.SubscribeToWaveSpanwer(this);
    }

    private void OnDisable()
    {
        GameManager.Instance.UnsubscribeToWaveSpawner(this);
    }

    #endregion
    #region Public Functions



    #endregion

    #region Private Functions
    private bool HasSequenceRunning()
    {
        return numRunningSequences > 0;
    }

    private bool HasNextWave()
    {
        return currentWaveIndex < levelWaves.Count;
    }

    private IEnumerator SpawnWave(Wave wave)
    {

        while(wave.NumOfSequencesLeft > 0)
        {
            var sequences = wave.GetNextSequences();

            foreach(var sequence in sequences)
            {
                StartCoroutine(SpawnSequence(sequence));
            }

            yield return new WaitUntil(() => numRunningSequences <= 0);
        }

        countDown = timeToNextWave;

    }

    private IEnumerator SpawnSequence(EnemySequence sequence)
    {
        numRunningSequences += 1;

        for(int i = 0; i < sequence.amountOfEnemies; i++)
        {
            var enemy = Instantiate(sequence.prefab, spawnTransform.position, spawnTransform.rotation);

            var enemyAiController = enemy.GetComponent<EnemyAIController>();
            enemyAiController.SetDestination(endTransform.position);



            yield return new WaitForSeconds(1 / sequence.spawnRate);
        }

        numRunningSequences -= 1;
    }

    private void ActivateWaves()
    {
        foreach(Wave wave in levelWaves)
        {
            wave.PassArrayToQueue();
        }
    }
    
    #endregion



}
