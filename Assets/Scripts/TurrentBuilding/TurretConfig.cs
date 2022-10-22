using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TurretConfig")]
public class TurretConfig : ScriptableObject
{
    public float attackRange;
    public Vector3 idleRotateVector = new Vector3(0, 1, 0);
    public float rotationSpeed = 100f;

    public float fireInteval = 0.5f;

    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public int cost;
    public int damage;

    [Header("For projectile turret")]
    public float muzzleSpeed;
}
