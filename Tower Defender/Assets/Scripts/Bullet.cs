using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Bullet : MonoBehaviour
{

    public IEnemy targetToFollow = null;
    [HideInInspector] public float damageMultiplier = 1f;
    [HideInInspector] public float speed = 10f;

    public float TotalDamage { get => baseDamage * damageMultiplier; private set { } }
    private float baseDamage = 10;
    private float lifeSpan = 2f;

    [SerializeField] private GameObject hitParticle = null;

    private void OnEnable()
    {
 
        StartCoroutine(LifeSpan(lifeSpan));
 
    }

    private void FixedUpdate()
    {
        UpdateDirection();
        //CheckPossibleCollision();
        Move();

    }

    private void Move() 
    {

        float moveDistance = speed * Time.deltaTime;
        transform.Translate(transform.forward * moveDistance, Space.World);

    }

    private void CheckPossibleCollision()
    {
        float moveDistance = speed * Time.deltaTime;
        RaycastHit hit;

        //Debug.DrawRay(transform.position, transform.forward * moveDistance, Color.red, 0.5f);
        if (Physics.Raycast(transform.position, transform.forward, out hit, moveDistance))
        {
            if(hit.collider.CompareTag("Enemy"))
            {
                // Hit an enemy
                IEnemy enemy = hit.collider.GetComponentInParent<IEnemy>();
                enemy.Health.TakeDamage(TotalDamage);
                BulletPool.Instance.ReturnToPool(this);

            }

        }
    }


    private void UpdateDirection()
    {
        if(targetToFollow.GameObject.activeSelf)
            transform.forward = (targetToFollow.Transform.position - transform.position).normalized;
    }

    private IEnumerator LifeSpan(float lifeSpan)
    {
        yield return new WaitForSeconds(lifeSpan);

        BulletDie();

    }

    private void BulletDie()
    {

        ParticleSystem bulletHitParticle = BulletHitPool.Instance.GetObject();
        bulletHitParticle.transform.position = transform.position;
        bulletHitParticle.transform.rotation = transform.rotation;
        bulletHitParticle.gameObject.SetActive(true);

        BulletPool.Instance.ReturnToPool(this);

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Enemy"))
        {
            IEnemy enemy = other.GetComponentInParent<IEnemy>();
            enemy.Health.TakeDamage(TotalDamage);
            BulletDie();
        }

    }
}
