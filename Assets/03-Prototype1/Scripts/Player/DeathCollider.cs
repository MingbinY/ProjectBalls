using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CollectionController>())
        {
            Destroy(other.gameObject);
            FindObjectOfType<CollectionSpawner>().SpawnRandomPickup();
        }
        else if (other.GetComponent<PlayerController>())
        {
            other.transform.position = Vector3.zero;
        }
    }
}
