using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public Ability currentAbility;

    public float cooldown;
    float activeTimer;
    public float currentCooldown;
    enum AbilityState
    {
        Ready,
        Active,
        Cooldown
    }

    [SerializeField]AbilityState currentState;

    private void Start()
    {
        currentState = AbilityState.Ready;
        currentCooldown = 0;
    }

    private void Update()
    {
        if (currentAbility != null)
        {
            UpdateAbilityState();
        }
    }

    public void OnAbility()
    {
        if (currentAbility != null)
        {
            ActivateAbility();
        }
    }

    void UpdateAbilityState()
    {
        switch (currentState)
        {
            case AbilityState.Ready:
                break;

            case AbilityState.Active:
                if (activeTimer > 0)
                {
                    activeTimer -= Time.deltaTime;
                }
                else
                {
                    currentCooldown = currentAbility.cooldown;
                    currentState = AbilityState.Cooldown;
                }
                break;
            case AbilityState.Cooldown:
                if (currentCooldown > 0)
                {
                    currentCooldown -= Time.deltaTime;
                }
                else
                {
                    currentCooldown = 0;
                    currentState = AbilityState.Ready;
                }
                break;
        }
    }
    public void ActivateAbility()
    {
        Debug.Log("Active");
        if (currentAbility != null && currentState == AbilityState.Ready)
        {
            //Active ability
            currentAbility.Activate(gameObject);
            activeTimer = currentAbility.duration;
            currentState = AbilityState.Active;
        }
    }
}
