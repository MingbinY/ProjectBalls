using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    normal,
    bonus,
    explosion,
    ability
}
public class CollectionController : MonoBehaviour
{
    public PickupType pickupType;

    [Header("Ability Pickup")]
    public Ability[] pickupAbility;

    [Header("Explosive Pickup")]
    public GameObject explosionVFX;

    [Header("General")]
    public int score = 1;
    CollectionSpawner spawner;
    CameraShake cameraShaker;
    public float shakeDuration;
    public float shakeMagitude;

    private void Start()
    {
        cameraShaker = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>(); 
        spawner = FindObjectOfType<CollectionSpawner>();
        if (pickupType == PickupType.explosion)
        {
            spawner.SpawnPickup();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Player")
        {
            if (pickupType == PickupType.normal)
            {
                other.GetComponent<PlayerController>().score += score;
            }else if(pickupType == PickupType.bonus)
            {
                other.GetComponent<PlayerController>().score += 2 * score;
            }
            else if (pickupType == PickupType.explosion)
            {
                Instantiate(explosionVFX, transform.position, Quaternion.identity);
                other.GetComponent<HealthManager>().TakeDamage();
                cameraShaker.ActiveShake(shakeDuration, shakeMagitude);
            }else if (pickupType == PickupType.ability)
            {
                if (pickupAbility != null)
                {
                    other.GetComponent<PlayerController>().score += score;
                    Ability ability = ChooseAbility(other);
                    other.GetComponent<AbilityHolder>().currentAbility = ability;
                }
            }

            if (Random.Range(0, 100) < 10)
            {
                spawner.SpawnAbilityPickup();
            }
            else
            {
                spawner.SpawnRandomPickup();
            }
            Destroy(gameObject);
        }
    }

    Ability ChooseAbility(Collider playerCol)
    {
        Ability playerAbility = playerCol.GetComponent<AbilityHolder>().currentAbility;
        int ind = Random.Range(0, pickupAbility.Length);
        while (pickupAbility[ind] == playerAbility)
        {
            ind = Random.Range(0, pickupAbility.Length);
        }

        return pickupAbility[ind];
    }
}
