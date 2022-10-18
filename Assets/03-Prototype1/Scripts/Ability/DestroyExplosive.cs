using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DestroyExplosive : Ability
{
    public float radius = 5f;
    public LayerMask mask;
    public GameObject explosionVFX;

    public override void Activate(GameObject parent)
    {
        //Instantiate(VFX, parent.transform.position, Quaternion.identity);
        Collider[] hits = Physics.OverlapSphere(parent.transform.position, radius, mask);
        foreach (Collider col in hits)
        {
            Instantiate(explosionVFX, parent.transform.position, Quaternion.identity);
            CollectionController cc = col.gameObject.GetComponent<CollectionController>();
            if (cc)
            {
                if (cc.pickupType == PickupType.explosion)
                {
                    Destroy(cc.gameObject);
                }
            }
        }
    }
}
