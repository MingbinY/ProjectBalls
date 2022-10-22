using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    private void Awake()
    {
        instance = this;
    }
    public void DestroyAfterDeath(GameObject enemyToDestroy)
    {
        Destroy(enemyToDestroy, 5f);
    }
}
