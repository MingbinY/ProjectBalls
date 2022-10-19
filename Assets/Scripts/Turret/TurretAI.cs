using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurretState
{
    Idle,
    Attack
}

public class TurretAI : MonoBehaviour
{
    TurretSensor sensor;
    public TurretConfig config;
    TurretState state = TurretState.Idle;
    public GameObject top;
    float nextFireTime;

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
            return;
        }
        //Face Target
        top.transform.LookAt(sensor.targetRef.transform);
        //Shoot Target
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + config.fireInteval;
            Debug.Log("Turret Shoot");
        }
    }
}
