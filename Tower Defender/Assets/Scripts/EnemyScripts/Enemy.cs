using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    public Health Health { get; set; }
    public Transform Transform { get => transform; set { } }
    public GameObject GameObject { get => this.gameObject; set { } }


    private void Awake()
    {
        Health = GetComponent<Health>();
    }

    private void Start()
    {

        EnemyManager.Instance.RegisterEnemy(this);
        
    }

    private void OnEnable()
    {
        Health.onDeath += OnDeath;
    }

    private void OnDisable()
    {
        Health.onDeath -= OnDeath;
    }

    private void OnDeath()
    {
        EnemyManager.Instance.UnregisterEnemy(this);
        GameObject.SetActive(false);
    }

}
