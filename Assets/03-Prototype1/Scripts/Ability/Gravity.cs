using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Gravity : Ability
{
    public float radius = 10f;
    public float dragForce = 50f;
    public LayerMask mask;

    public override void Activate(GameObject parent)
    {
        Debug.Log("DRAG");
        Collider[] cols = Physics.OverlapSphere(parent.transform.position, radius, mask);
        Debug.Log(cols.Length);
        foreach(Collider col in cols)
        {
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if (rb && col.GetComponent<CollectionController>().pickupType != PickupType.explosion)
            {
                rb.AddForce((parent.transform.position - col.transform.position).normalized * dragForce, ForceMode.Impulse);
            }
        }

    }
}
