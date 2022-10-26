using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCrate : MonoBehaviour
{
    public List<Gun> gunList;
    public Gun gunToObtain;

    private void Awake()
    {
        gunToObtain = gunList[Random.Range(0,gunList.Count)];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            // When player enters
            GunController controller = FindObjectOfType<GunController>();
            if (controller != null)
            {
                for (int i = 0; i < gunList.Count; i++)
                {
                    gunToObtain = gunList[i];
                    if (controller.gunList.Contains(gunToObtain))
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (!controller.gunList.Contains(gunToObtain))
                controller.AddGun(gunToObtain);
        }
        Destroy(gameObject);
    }
}
