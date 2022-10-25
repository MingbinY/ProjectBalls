using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "TurretConfig")]
public class TurretConfig : ScriptableObject
{
    public Sprite turretIcon;
    public string turretName;

    [Header("Sensor Configs")]
    public float sensorAngle;
    public float attackRange;
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [Header("Idle Rotation Configs")]
    public Vector3 idleRotateVector = new Vector3(0, 1, 0);
    public float rotationSpeed = 100f;
    
    [Tooltip("The Inteval of each round of attack or dealing damage")]
    public float fireInteval = 0.5f;

    public int buildCost;
    public int damage;

    [Header("For Projectile Turret")]
    public float muzzleSpeed;
    public Bullet bullet;

    [Header("For Laser Turret")]
    public GameObject laserPrefab;

    [Header("For Missile Turret")]
    public float explosionRadius;
    public GameObject missilePrefab;
    public float missileFireInteval = 0.5f;
    
}
