using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Base Enemy Config")]
public class EnemyConfig : ScriptableObject
{
    public float chaseSpeed;
    public float viewRange;
    public float attackRange;
    public float attackStoppingDistance;
}
