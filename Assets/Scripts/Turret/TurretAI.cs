using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurretState
{
    Idle,
    Attack
}

public enum TurretType
{
    projectile,
    laser,
    missile
}

public class TurretAI : MonoBehaviour
{
    public TurretType type;
    TurretSensor sensor;
    public TurretConfig config;
    TurretState state = TurretState.Idle;
    public GameObject top;
    float nextFireTime;
    public float projectile;

    private void Awake()
    {
        sensor = GetComponentInChildren<TurretSensor>();
        top = GetComponentInChildren<TurretSensor>().gameObject;
        sensor.targetMask = config.targetMask;
        sensor.radius = config.attackRange;
        sensor.obstacleMask = config.obstacleMask;
        nextFireTime = Time.time;
    }

    private void Update()
    {
        switch (state)
        {
            case TurretState.Idle:
                IdleUpdate();
                break;
            case TurretState.Attack:
                AttackUpdate();
                break;
        }
    }

    void IdleUpdate()
    {
        //Rotate around
        if (sensor.targetInSight)
        {
            state = TurretState.Attack;
            return;
        }
        top.transform.Rotate(config.idleRotateVector);
    }

    void AttackUpdate()
    {
        //Check if target in sight
        if (!sensor.targetInSight)
        {
            state = TurretState.Idle;
            top.transform.Rotate(new Vector3(0, top.transform.rotation.y, 0));
            return;
        }
        //Face Target
        top.transform.rotation = Quaternion.LookRotation(sensor.targetRef.transform.position - top.transform.position);
        //Shoot Target
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + config.fireInteval;
            Debug.Log("Turret Shoot");
        }
    }
}
