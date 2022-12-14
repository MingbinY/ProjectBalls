using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public float power = 0.7f;
    public float duration = 1.0f;
    public Transform cameraShake;
    public float slowDownAmount = 1.0f;
    public bool shouldShake = false;

    Vector3 startPosition;
    float initialDuration;

	// Use this for initialization
	void Start () {
        cameraShake = Camera.main.transform;
        startPosition = cameraShake.localPosition;
        initialDuration = duration;
	}
	
	// Update is called once per frame
	void Update () {
        if (shouldShake)
        {
            if(duration > 0)
            {
                cameraShake.localPosition = startPosition + Random.insideUnitSphere * power;
                duration -= Time.deltaTime * slowDownAmount;
            }
            else
            {
                shouldShake = false;
                duration = initialDuration;
                cameraShake.localPosition = startPosition;
            }
        }
		
	}
}
