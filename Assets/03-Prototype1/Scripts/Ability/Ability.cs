using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public string abilityName;
    public float cooldown;
    public float duration;

    public virtual void Activate(GameObject parent) { }

}
