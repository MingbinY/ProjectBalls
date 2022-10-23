using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float selfDestroyTime = 1f;
    private void Awake()
    {
        Destroy(gameObject, selfDestroyTime);
    }
}
