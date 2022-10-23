using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxPlay : MonoBehaviour {

    public GameObject FirePoint;
    public List<GameObject> vfx = new List<GameObject> ();
    public RotateToMouse rotateToMouse;
    

    private GameObject EffectToSpawn;
    private float timeToFire = 0;
    protected bool letPlay = true;
     

	// Use this for initialization
	void Start () {
        EffectToSpawn = vfx[0];
        
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton (0) && Time.time >= timeToFire)
        {
            
            EffectToSpawn.SetActive(true);
            {
                letPlay = !letPlay;
            }

            if (letPlay)
            {
                if (EffectToSpawn.GetComponent<ParticleSystem>().isPlaying)
                {
                    EffectToSpawn.GetComponent<ParticleSystem>().Play();
                }
            }
            else
            {
                if (EffectToSpawn.GetComponent<ParticleSystem>().isPlaying)
                {
                    EffectToSpawn.GetComponent<ParticleSystem>().Stop();
                }
            }
            timeToFire = Time.time + 1 / EffectToSpawn.GetComponent<ProjectileMove>().fireRate;
            SpawnVFX();
        }
	}

    void SpawnVFX ()
    {
        GameObject vfx;

        if (FirePoint != null)
        {
            vfx = Instantiate(EffectToSpawn, FirePoint.transform.position, Quaternion.identity);
            if (rotateToMouse != null)
            {
                vfx.transform.localRotation = rotateToMouse.GetRotation();
            }
        }

        else
        {
            Debug.Log("No Fire Point");
        }
    }
}
