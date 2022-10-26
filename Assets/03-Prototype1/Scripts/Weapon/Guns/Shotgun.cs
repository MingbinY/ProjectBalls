using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    public int pelletCount;
    public float spreadAngle;
    List<Quaternion> pellets;

    private void Awake()
    {
        pellets = new List<Quaternion>(pelletCount);
        for (int i = 0; i < pelletCount; i++)
        {
            pellets.Add(Quaternion.Euler(Vector3.zero));
        }
    }

    public override void Shoot(BulletSource bs)
    {
        if (Time.time > nextShotTime && canShoot)
        {
            nextShotTime = Time.time + msBetweenShots/1000;
            ShotgunShootProjectile(bs);
        }
    }

    public void ShotgunShootProjectile(BulletSource bs)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(shootSFX);
        }
        for (int i = 0; i < pelletCount; i++)
        {
            pellets[i] = Random.rotation;
            Quaternion q = pellets[i];
            q.x = 0;
            q.z = 0;
            Bullet newProjectile = Instantiate(bullet, muzzle.position, transform.rotation);
            newProjectile.transform.rotation = Quaternion.RotateTowards(newProjectile.transform.rotation, q, spreadAngle);
            newProjectile.SetSource(bs);
            newProjectile.SetDamage(damage);
            newProjectile.SetSpeed(muzzleVelocity);
        }
    }
}
