using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;
    public int currentMoney;

    private void Awake()
    { //Singleton
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    public void EarnMoney(int moneyGained)
    {
        currentMoney += moneyGained;
    }

    public void OnSceneReload()
    {
        currentMoney = 0;
    }
}
