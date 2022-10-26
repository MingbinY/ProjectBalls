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
    private EnemyHealthManager healthManager;

    public EnemyStates state;
    public EnemyStates initState;

    private Transform playerTransform;

    public Gun weapon;
    public GameObject weaponHolder;

    private void Start()
    {
        sensor = GetComponent<AISensor>();
        agent = GetComponent<NavMeshAgent>();
        healthManager = GetComponent<EnemyHealthManager>();
        state = initState;
        playerTransform = FindObjectOfType<PlayerController>().transform;
        sensor.radius = config.viewRange;
        if (weapon)
        {
            Gun equipGun = Instantiate(weapon, weaponHolder.transform.position, Quaternion.identity);
            equipGun.gameObject.transform.parent = weaponHolder.transform;
            weapon = equipGun;
            weapon.b_source = BulletSource.Enemy;
        }
    }

    private void Update()
    {
        if (healthManager.isDead)
            return;
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

    public virtual void ChaseUpdate()
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

        agent.stoppingDistance = 2;
        agent.speed = config.chaseSpeed;
        agent.SetDestination(playerTransform.position);
    }

    public virtual void AttackUpdate()
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

    public virtual void Attack()
    {
        //Place holder
        weapon.Shoot(BulletSource.Enemy);
    }

    void FacePlayer()
    {
        //Face the direction of player
        Vector3 playerPos = playerTransform.position;
        playerPos.y = transform.position.y;
        transform.LookAt(playerPos);
    }

    bool PlayerInAttackRange()
    {
        return Vector3.Distance(playerTransform.position, transform.position) <= config.attackRange;
    }
}
