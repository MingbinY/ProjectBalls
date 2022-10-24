using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : BasicHealthManager
{
    public int moneyDrop = 10;

    public event System.Action OnDeath;
    public override void Death()
    {
        base.Death();
        if (OnDeath != null)
        {
            OnDeath();
        }
        MoneyManager.instance.EarnMoney(moneyDrop);
        EnemyManager.instance.DestroyAfterDeath(gameObject);
        gameObject.SetActive(false);
    }
}
