using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTurretAI : TurretAI
{
    public Bullet projectile;

    public override void Attack()
    {
        if (Time.time < nextFireTime)
        {
            //in cool down
            return;
        }

        nextFireTime = Time.time + config.fireInteval;
        // Shoot projectile
        foreach (Transform muzzle in fireControl.muzzles)
        {
            //Shoot
            Bullet newProjectile = Instantiate(projectile, muzzle.transform.position, top.transform.rotation);
            newProjectile.SetSource(BulletSource.Player);
            newProjectile.SetDamage(config.damage);
            newProjectile.SetSpeed(config.muzzleSpeed);
        }
    }
}
