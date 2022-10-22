using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurretAI : TurretAI
{
    public GameObject laserPrefab;
    private GameObject Instance;

    private Hovl_Laser LaserScript;
    private Hovl_Laser2 LaserScript2;

    bool isEnabled = false;

    public override void Awake()
    {
        bool isEnabled = false;
        base.Awake();
    }

    public override void Update()
    {
        switch (state)
        {
            case TurretState.Idle:
                if (isEnabled)
                    DisableLaser();
                break;
        }
        base.Update();
    }

    public override void Attack()
    {
        if (!sensor.targetInSight)
        {
            DisableLaser();
            return;
        }

        if (!isEnabled)
            EnableLaser();

        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + config.fireInteval;
            sensor.targetRef.GetComponent<BasicHealthManager>().TakeDamage(config.damage);
        }
    }

    void EnableLaser()
    {
        isEnabled = true;
        Destroy(Instance);
        Instance = Instantiate(laserPrefab, fireControl.muzzles[0].position, fireControl.muzzles[0].transform.rotation);
        Instance.transform.parent = top.transform;
        LaserScript = Instance.GetComponent<Hovl_Laser>();
        LaserScript2 = Instance.GetComponent<Hovl_Laser2>();
    }

    void DisableLaser()
    {
        isEnabled = false;
        Destroy(Instance);
    }
}
