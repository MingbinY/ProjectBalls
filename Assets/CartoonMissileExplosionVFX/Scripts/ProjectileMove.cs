using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour {

    public float speed;
    public float fireRate;
    public GameObject hitPrefab;
    public CameraShake cameraShake;
    protected bool letPlay = true;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("No Speed");
        }

    }

    void OnCollisionEnter (Collision co)
    {
        speed = 0;
        hitPrefab.SetActive(true);
        cameraShake.shouldShake = true;
        {
            letPlay = !letPlay;
        }

        if (letPlay)
        {
            if (!hitPrefab.GetComponent<ParticleSystem>().isPlaying)
            {
                hitPrefab.GetComponent<ParticleSystem>().Play();
            }
        }
        else
        {
            if (hitPrefab.GetComponent<ParticleSystem>().isPlaying)
            {
                hitPrefab.GetComponent<ParticleSystem>().Stop(); 
            }
        }
        ContactPoint contact = co.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;

        if (hitPrefab != null)
        {
            var hitVFX = Instantiate(hitPrefab, pos, rot);
            var psHit = hitVFX.GetComponent<ParticleSystem>();
            if (psHit != null)
                Destroy(hitVFX, psHit.main.duration);
            else
            {
                var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitVFX, psChild.main.duration);
            }
        }
        Destroy(gameObject);
        
    }
}
