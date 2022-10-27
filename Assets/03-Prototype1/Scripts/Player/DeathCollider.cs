using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BasicHealthManager>() != null)
        {
            other.GetComponent<BasicHealthManager>().TakeDamage(10000000);
        }
    }
}
