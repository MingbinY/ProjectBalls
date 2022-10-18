using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleRotation : MonoBehaviour
{
    public Vector3 rotationSpeed;
    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
