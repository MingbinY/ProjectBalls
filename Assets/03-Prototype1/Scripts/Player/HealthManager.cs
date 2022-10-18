using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public int health = 10;
    public TMP_Text text;

    private void Start()
    {
        text.text = "Health: " + health.ToString();
    }

    private void Update()
    {
        UpdateHealth();    
    }

    void UpdateHealth()
    {
        text.text = "Health: " + health.ToString();
        if (health <= 0)
        {
            Destroy(gameObject);
            FindObjectOfType<InGameMenu>().GameOver();
        }
    }

    public void TakeDamage()
    {
        health--;
    }
}
