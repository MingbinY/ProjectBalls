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
    public TurretSensor sensor;
    public TurretConfig config;
    public TurretFireControl fireControl;
    public TurretState state = TurretState.Idle;
    public GameObject top;
    public float nextFireTime;
    

    public virtual void Awake()
    {
        sensor = GetComponentInChildren<TurretSensor>();
        top = GetComponentInChildren<TurretSensor>().gameObject;
        sensor.targetMask = config.targetMask;
        sensor.radius = config.attackRange;
        sensor.obstacleMask = config.obstacleMask;
        nextFireTime = Time.time;
        fireControl = top.GetComponent<TurretFireControl>();
    }

    public virtual void Update()
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

    public virtual void IdleUpdate()
    {
        //Rotate around
        if (sensor.targetInSight)
        {
            state = TurretState.Attack;
            return;
        }
        top.transform.Rotate(config.idleRotateVector);
    }

    public virtual void AttackUpdate()
    {
        //Check if target in sight
        if (!sensor.targetInSight)
        {
            Quaternion targetRotation = top.transform.rotation;
            targetRotation.x = 0;
            targetRotation.z = 0;
            top.transform.rotation = Quaternion.RotateTowards(top.transform.rotation, targetRotation, Time.deltaTime * config.rotationSpeed);
            state = TurretState.Idle;
            return;
        }
        //Face Target
        top.transform.rotation = Quaternion.LookRotation(sensor.targetRef.transform.position - top.transform.position);
        //Shoot Target
        Attack();
    }

    public virtual void Attack()
    {
    }
}
