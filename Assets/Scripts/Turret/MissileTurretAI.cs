using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTurretAI : TurretAI
{
    public bool inAttackCoroutine;
    public override void Attack()
    {
        if (inAttackCoroutine || Time.time < nextFireTime)
        {
            return;
        }

        inAttackCoroutine = true;
        StartCoroutine(MissileAttackCoroutine());

    }

    IEnumerator MissileAttackCoroutine()
    {
        foreach (Transform muzzle in fireControl.muzzles)
        {
            GameObject newMissile = Instantiate(config.missilePrefab, muzzle.position, muzzle.rotation);
            newMissile.GetComponent<Missile>().targetPos = sensor.targetRef.transform.position;
            yield return new WaitForSeconds(config.missileFireInteval);
        }
        
        nextFireTime = Time.time + config.fireInteval;
        inAttackCoroutine=false;
    }
}
