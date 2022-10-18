using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerHealthManager : BasicHealthManager
{
    public Image healthBar;
    public GameObject gameOverUI;

    public float lastHitTImer;
    public float timeBeforeRegenerate = 0.5f;
    public float regenMultiplier = 5;

    private void Update()
    {
        if (lastHitTImer > 0)
        {
            lastHitTImer -= Time.deltaTime;
        }

        if (lastHitTImer <= 0 && currentHealth < maxHealth)
        {
            currentHealth += regenMultiplier * Time.deltaTime;
            healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        lastHitTImer = timeBeforeRegenerate;
        healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }

    public override void Death()
    {
        base.Death();
        GetComponent<PlayerController>().GetComponent<PlayerInput>().enabled = false;
        gameOverUI.SetActive(true);
    }
}
