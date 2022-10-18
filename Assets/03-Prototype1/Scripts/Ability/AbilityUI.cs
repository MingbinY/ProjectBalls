using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityUI : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text abilityName;
    public TMP_Text abilityCooldownText;
    public Image abilityImage;
    public Image cooldownIMG;

    [Header("Components")]
    public AbilityHolder abilityHolder;

    private void Awake()
    {
        abilityHolder = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityHolder>();
    }

    private void Update()
    {
        HandleAbilityCooldown();
        HandleAbilityName();
    }
    public void HandleAbilityName()
    {
        if (abilityHolder.currentAbility == null)
        {
            abilityName.text = "Reach 10 Pts";
        }
        else
        {
            abilityName.text = abilityHolder.currentAbility.abilityName;
        }
    }

    public void HandleAbilityCooldown()
    {
        cooldownIMG.fillAmount = abilityHolder.currentCooldown / abilityHolder.currentAbility.cooldown;
        if (abilityHolder.currentAbility == null)
        {
            abilityCooldownText.text = "No Ability";
        }
        else if (abilityHolder.currentCooldown == 0)
        {
            abilityCooldownText.text = "";
            Color color = abilityImage.color;
            color.a = 1f;
            abilityImage.color = color;
        }
        else
        {
            Color color = abilityImage.color;
            color.a = 0.3f;
            abilityImage.color = color;
            float dispCD = Mathf.Round(abilityHolder.currentCooldown * 10.0f) * 0.1f;
            abilityCooldownText.text = dispCD.ToString();
        }
    }
    
}
