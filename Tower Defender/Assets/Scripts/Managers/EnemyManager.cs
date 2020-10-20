using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : Singleton<EnemyManager>
{

    [SerializeField]
    private List<IEnemy> enemiesList;
    public int numEnemiesTotal => enemiesList.Count;
    public int numEnemiesAlive = 0;
    

    private void Awake()
    {
        enemiesList = new List<IEnemy>();
    }

    public void RegisterEnemy(IEnemy enemy)
    {
        enemiesList.Add(enemy);
        numEnemiesAlive++;
    }

    public void UnregisterEnemy(IEnemy enemy)
    {
        enemiesList.Remove(enemy);
        numEnemiesAlive--;
    }

    public IEnemy GetClosestEnemy(Vector3 targetPoint)
    {

        IEnemy closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach(IEnemy enemy in enemiesList)
        {

            if (enemy.GameObject.activeSelf)
            {
                float auxDistance = Vector3.Distance(targetPoint, enemy.Transform.position);

                if(auxDistance < closestDistance)
                {
                    closestDistance = auxDistance;
                    closestEnemy = enemy;
                }
            }
        }

        return closestEnemy;

    }

    public IEnemy GetFarthestEnemyInRange(Vector3 targetPoint, float range)
    {
        IEnemy farthestEnemy = null;
        float farthestDistance = 0;

        foreach (IEnemy enemy in enemiesList)
        {
            if (enemy.GameObject.activeSelf)
            {
                float auxDistance = Vector3.Distance(targetPoint, enemy.Transform.position);

                if ((auxDistance <= range) && (farthestDistance < auxDistance))
                {
                    farthestDistance = auxDistance;
                    farthestEnemy = enemy;
                }
            }
        }

        return farthestEnemy;
    }
}