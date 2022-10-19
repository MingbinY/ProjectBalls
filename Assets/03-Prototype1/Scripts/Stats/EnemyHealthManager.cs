using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : BasicHealthManager
{
    public int moneyDrop = 10;
    public override void Death()
    {
        base.Death();
        MoneyManager.instance.EarnMoney(moneyDrop);
        Destroy(gameObject);
    }
}
