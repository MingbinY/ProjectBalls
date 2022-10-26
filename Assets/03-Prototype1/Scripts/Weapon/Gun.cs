using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip shootSFX;
    public AudioClip boltClip;
    public BulletSource b_source;

    public string gunName;
    public Sprite gunSprite;

    public int bulletPerMag = 12;
    public int bulletInMag;
    public float reloadTime = 3f;
    public float reloadTimer;
    public bool canShoot = true;
    public bool reloading = false;

    public Transform muzzle;
    public Bullet bullet;
    public float msBetweenShots = 100;
    public float muzzleVelocity = 40;

    public int damage = 20;

    public float nextShotTime;

    private void Start()
    {
        bulletInMag = bulletPerMag;
        nextShotTime = Time.time;
        reloading = false;
        canShoot = true;
        audioSource = GetComponent<AudioSource>();
    }

    public IEnumerator Reload()
    {
        
        reloading = true;
        canShoot = false;
        yield return new WaitForSeconds(reloadTime);
        bulletInMag = bulletPerMag;
        canShoot=true;
        reloading = false;
    }

    public virtual void Shoot(BulletSource bs)
    {
        if (Time.time > nextShotTime && canShoot)
        {
            bulletInMag--;
            nextShotTime = Time.time + msBetweenShots / 1000;
            Bullet newProjectile = Instantiate(bullet, muzzle.transform.position, transform.rotation);
            newProjectile.SetSource(bs);
            newProjectile.SetDamage(damage);
            newProjectile.SetSpeed(muzzleVelocity);
            StartCoroutine(gunSfxSequence());
        }

        if (bulletInMag == 0 && !reloading)
        {
            StartCoroutine(Reload());
        }
    }

    IEnumerator gunSfxSequence()
    {
        audioSource.PlayOneShot(shootSFX);
        if (boltClip != null)
        {
            while (audioSource.isPlaying && bulletInMag > 0)
            {
                yield return null;
            }
            audioSource.PlayOneShot(boltClip);
        }
    }

}
