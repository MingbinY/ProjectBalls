using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;
    public Text moneyText;
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

    public void FixedUpdate()
    {
        moneyText.text = currentMoney.ToString();
    }

    public void OnSceneReload()
    {
        currentMoney = 0;
    }
}
