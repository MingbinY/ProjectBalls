using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : BasicHealthManager
{
    public override void Death()
    {
        base.Death();
        Destroy(gameObject);
    }
}
