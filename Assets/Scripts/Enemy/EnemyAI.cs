using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates
{
    Idle,
    Chase,
    Attack
}
[RequireComponent(typeof(AISensor))]
public class EnemyAI : MonoBehaviour
{
    AISensor sensor;
    private NavMeshAgent agent;
    public EnemyConfig config;

    public EnemyStates state;
    public EnemyStates initState;

    private Transform playerTransform;

    public Gun weapon;

    private void Start()
    {
        sensor = GetComponent<AISensor>();
        agent = GetComponent<NavMeshAgent>();
        state = initState;
        playerTransform = FindObjectOfType<PlayerController>().transform;
        sensor.radius = config.viewRange;
    }

    private void Update()
    {
        //State selection
        switch (state)
        {
            case EnemyStates.Idle:
                IdleUpdate();
                break;
            case EnemyStates.Chase:
                ChaseUpdate();
                break;
            case EnemyStates.Attack:
                AttackUpdate();
                break;
        }
    }

    void IdleUpdate()
    {
        //do nothing
        //place holder
        if (sensor.playerInSight)
        {
            state = EnemyStates.Chase;
        }
    }

    void ChaseUpdate()
    {
        agent.updateRotation = true;
        if (sensor.playerInSight && PlayerInAttackRange())
        {
            //player in attack range and player in sight
            agent.ResetPath();
            agent.stoppingDistance = config.attackStoppingDistance;
            agent.SetDestination(playerTransform.position);
            state = EnemyStates.Attack;
            return;
        }

        agent.stoppingDistance = 0;
        agent.speed = config.chaseSpeed;
        agent.SetDestination(playerTransform.position);
    }

    void AttackUpdate()
    {
        if (!PlayerInAttackRange() || !sensor.playerInSight)
        {
            //Player run away or hide, return to chase player
            state = EnemyStates.Chase;
            return;
        }

        FacePlayer();
        Attack();
    }

    void Attack()
    {
        //Place holder
        weapon.Shoot(BulletSource.Enemy);
    }

    void FacePlayer()
    {
        //Face the direction of player
        transform.LookAt(playerTransform.position);
    }

    bool PlayerInAttackRange()
    {
        return Vector3.Distance(playerTransform.position, transform.position) <= config.attackRange;
    }
}
