using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHealthManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip takeHitClip;
    public AudioClip deathClip;
    public int maxHealth = 100;
    public float currentHealth;
    public bool isDead = false;

    public virtual void Awake()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }
    public virtual void TakeDamage(int damage)
    {
        if (isDead)
            return;

        if (takeHitClip)
            audioSource.PlayOneShot(takeHitClip);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public virtual void Death()
    {
        if (deathClip)
            audioSource.PlayOneShot(deathClip);
    }
}
