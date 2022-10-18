using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_Bullet : Bullet
{
    public float explosionRadius = 5f;
    public GameObject explosionVFX;

    public override void OnTriggerEnter(Collider other)
    {
        Debug.Log("Rocket Trigger");
        //Explosion VFX
        Instantiate(explosionVFX, transform.position, Quaternion.identity);
        Collider[] cols = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider col in cols)
        {
            BasicHealthManager hm;
            if (b_source == BulletSource.Player)
            {
                hm = col.GetComponent<EnemyHealthManager>();
            }else if (b_source == BulletSource.Enemy)
            {
                damage = damage / 2;
                hm = col.GetComponent<PlayerHealthManager>();
            }
            else
            {
                hm = col.GetComponent<BasicHealthManager>();
            }

            if (hm)
            {
                hm.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}
