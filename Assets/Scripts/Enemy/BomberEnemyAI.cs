using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberEnemyAI : EnemyAI
{
    public bool isTriggered = false;
    public GameObject explosionVFX;
    public AudioSource audioSource;
    public AudioClip explosionSFX;

    public override void ChaseUpdate()
    {
        if (isTriggered)
            return;
        base.ChaseUpdate();
    }

    public override void AttackUpdate()
    {
        if (isTriggered)
            return;
        base.AttackUpdate();
    }

    public override void Attack()
    {
        isTriggered = true;
        StartCoroutine(BomberAttackCoroutine());
    }

    IEnumerator BomberAttackCoroutine()
    {
        float explodeDelay = 1;
        float flashSpeed = 4;

        Material mat = GetComponent<Renderer>().material;
        Color initialColor = mat.color;
        Color flashColor = Color.red;
        float explodeTimer = 0;

        while (explodeTimer < explodeDelay)
        {
            mat.color = Color.Lerp(initialColor, flashColor, Mathf.PingPong(explodeTimer * flashSpeed, 1));
            explodeTimer += Time.deltaTime;
            yield return null;
        }

        Explode();
    }

    void Explode()
    {
        if (explosionSFX)
            audioSource.PlayOneShot(explosionSFX);
        Instantiate(explosionVFX, transform.position, Quaternion.identity);
        Collider[] cols = Physics.OverlapSphere(transform.position, config.explosionRadius);
        if (cols.Length > 0)
        {
            foreach (Collider col in cols)
            {
                BasicHealthManager bhm = col.GetComponent<BasicHealthManager>();
                if (bhm != null)
                    bhm.TakeDamage(config.damage);
            }
        }

        GetComponent<EnemyHealthManager>().TakeDamage(100000);
    }
}
