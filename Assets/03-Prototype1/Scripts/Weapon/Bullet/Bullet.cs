using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletSource
{
    Player,
    Enemy
}

public class Bullet : MonoBehaviour
{
    public float speed = 10;
    public int damage = 10;

    public BulletSource b_source;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        Destroy(gameObject, 10f);
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    public void SetSource(BulletSource bulletSource)
    {
        b_source = bulletSource;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (b_source == BulletSource.Player)
        {
            EnemyHealthManager ehm = other.GetComponent<EnemyHealthManager>();
            if (ehm != null)
            {
                ehm.TakeDamage(damage);
            }
        }

        if (b_source == BulletSource.Enemy)
        {
            PlayerHealthManager phm = other.GetComponent<PlayerHealthManager>();
            if (phm != null)
            {
                phm.TakeDamage(damage/2);
            }
        }

        Destroy(gameObject);
    }
}
