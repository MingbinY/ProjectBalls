using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{
    public Image healthbarImg;
    EnemyHealthManager healthManager;
    Camera mainCam;

    private void Start()
    {
        healthManager = GetComponentInParent<EnemyHealthManager>();
        mainCam = Camera.main;
    }

    private void Update()
    {
        healthbarImg.fillAmount = (float)healthManager.currentHealth / (float)healthManager.maxHealth;
        LookAtCam();
    }

    void LookAtCam()
    {
        transform.rotation = mainCam.transform.rotation;
    }
}
