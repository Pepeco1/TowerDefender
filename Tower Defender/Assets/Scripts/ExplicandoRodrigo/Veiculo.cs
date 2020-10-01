using UnityEngine;

public class Veiculo : MonoBehaviour
{

    private void Start()
    {
        IAmInteractable enemyRanged = new EnemyRanged() as IAmInteractable;
        IAmInteractable enemyBrabo = new EnemyBrabo() as IAmInteractable;

        EnemyRanged[] listaEnemys = new EnemyRanged[2];

        EnemyRanged enemyRangedAux = new EnemyRanged();
        listaEnemys[0] = enemyRangedAux;
        
        //listaEnemys[0] = enemyRanged;
        //listaEnemys[1] = enemyBrabo;

        foreach(IAmInteractable enemy in listaEnemys)
        {
            enemy.Interact();
        }

    }

}

class EnemyRanged : EnemyReal, IAmInteractable
{

    public void Interact()
    {
        Debug.Log("Hi, I am Ranged!");
    }

    public override void TakeDamage()
    {

        vida -= 10;
        
    }
}

class EnemyBrabo : EnemyReal, IAmInteractable
{
    public void Interact()
    {
        Debug.Log("Hi, I am brabo!");
    }

    public override void TakeDamage() {

        vida -= 5;

    }

}

abstract class EnemyReal
{
    public int vida = 10;

    public abstract void TakeDamage();

}

interface IAmInteractable
{
    void Interact();
}
