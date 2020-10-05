using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Wave : ScriptableObject
{

    [SerializeField] private EnemySequence[] enemySequences = null;
    private Queue<EnemySequence> sequenceQueue = new Queue<EnemySequence>();
    public int NumOfSequencesLeft { get => sequenceQueue.Count; private set {} }

    public void PassArrayToQueue()
    {
        foreach(EnemySequence sequence in enemySequences)
        {
            sequenceQueue.Enqueue(sequence);
        }
    }

    public List<EnemySequence> GetNextSequences()
    {
        List<EnemySequence> sequenceList = new List<EnemySequence>();

        bool nextHasJoinBehavior = false;

        do
        {
            sequenceList.Add(sequenceQueue.Dequeue());


            EnemySequence nextSequence = null;
            if (NumOfSequencesLeft > 0)
            {

                nextSequence = sequenceQueue.Peek();

            }
            
            // nextSequence is null? if it is, nextHasJoinBehavior = false, else nextHasJoinBehavior = nextSequence.joinBehavior
            nextHasJoinBehavior = (nextSequence?.joinBehavior) != null && nextSequence.joinBehavior;


            
        } while (nextHasJoinBehavior);

        return sequenceList;

    }

}
