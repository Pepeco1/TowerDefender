using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAIController : MonoBehaviour
{

    private Vector3 m_destination = Vector3.zero;
    private NavMeshAgent m_navMeshAgent;
    private bool ReachedEnd => !m_navMeshAgent.hasPath;


    void Awake()
    {
        
        m_navMeshAgent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        if (ReachedEnd)
        {
            GetComponent<IEnemy>().Health.Die();
        }
    }

    public void SetDestination()
    {

         bool setDestination = m_navMeshAgent.SetDestination(m_destination);

        if(setDestination == false)
        {
            Debug.Log("[EnemyAiController] NavMeshAgent SetDestination returned false");
        }

    }

    public void SetDestination(Vector3 newDestination)
    {

        m_destination = newDestination;

        SetDestination();

    }


}
