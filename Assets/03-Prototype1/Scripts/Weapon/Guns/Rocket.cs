using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Gun
{
    public GameObject rocketInMuzzle;


    public override void Shoot(BulletSource bs)
    {
        if (canShoot)
        {
            rocketInMuzzle.SetActive(false);
            Invoke("ShowRocket", reloadTime);
        }

        base.Shoot(bs);
    }

    void ShowRocket()
    {
        rocketInMuzzle.SetActive(true);
    }
}
