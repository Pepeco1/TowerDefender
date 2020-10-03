using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    private Wave currentWave;
    [SerializeField] private float timeToNextWave = 3f;
    private float countDown = 0f;

    [SerializeField] private List<Wave> levelWaves = new List<Wave>();
    private int currentWaveIndex = 0;
    private int numRunningSequences = 0;

    [SerializeField]
    private Transform spawnTransform = null;
    [SerializeField]
    private Transform endTransform = null;

    private void Awake()
    {

         ActivateWaves();

    }

    private void Update()
    {
        
        if(numRunningSequences > 0)
        {
            return;
        }

        countDown -= Time.deltaTime;

        if(countDown <= 0)
        {
            if(currentWaveIndex < levelWaves.Count)
            {

                StartCoroutine(SpawnWave(levelWaves[currentWaveIndex]));
                currentWaveIndex++;
                countDown = Mathf.Infinity;

            }
        }

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

        countDown = 5f;

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

}
