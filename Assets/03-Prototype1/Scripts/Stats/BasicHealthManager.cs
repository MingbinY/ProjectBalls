using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHealthManager : MonoBehaviour
{
    public int maxHealth = 100;
    public float currentHealth;
    public bool isDead = false;

    public virtual void Awake()
    {
        currentHealth = maxHealth;
    }
    public virtual void TakeDamage(int damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public virtual void Death()
    {

    }
}
