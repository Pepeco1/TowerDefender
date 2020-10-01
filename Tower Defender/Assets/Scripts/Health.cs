using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, ITakeDamage
{

    [SerializeField] float maxHealth;
    private float currentHealth;

    public UnityAction onDeath = null;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount) 
    {

        currentHealth -= damageAmount;

        if (currentHealth < 0)
        {
            
            Die();

        }

    }

    public void Die()
    {
        if (onDeath != null)
            onDeath.Invoke();
    }

}
