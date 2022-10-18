using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    public BulletSource b_source = BulletSource.Player;

    public Gun startingGun;
    public Transform weaponHoldTransform;
    bool shootInput;
    public Player inputActions;
    Gun equippedGun;

    public List<Gun> gunList;
    public Gun prevGun;
    public Gun nextGun;

    bool nextGunInput;
    bool prevGunInput;
    public int currentGunIndex;

    [Header("UI")]
    public Image currentGunImage;
    public Image nextGunImage;
    public Image prevGunImage;
    public GameObject reloadingHint;
    public Text ammoHint;

    private void Start()
    {
        inputActions = FindObjectOfType<PlayerController>().inputActions;
        currentGunIndex = 0;
        if (gunList[currentGunIndex] != null)
        {
            EquipGun(gunList[currentGunIndex]);
        }
    }

    private void Update()
    {
        ammoHint.text = equippedGun.bulletInMag + " / " + equippedGun.bulletPerMag;
        reloadingHint.SetActive(equippedGun.reloading);
        ammoHint.gameObject.SetActive(!equippedGun.reloading);
        HandleShoot();
        HandleChangeWeapon();
    }

    public void HandleShoot()
    {
        //inputActions.Action.Shoot.performed += i => shootInput = true;
        shootInput = inputActions.Action.Shoot.IsPressed();
        if (shootInput)
        {
            equippedGun.Shoot(b_source);
            shootInput = false;
        }
    }

    public void HandleChangeWeapon()
    {
        inputActions.Action.NextGun.performed += i => nextGunInput = true;
        inputActions.Action.PrevGun.performed += i => prevGunInput = true;

        if (equippedGun.reloading)
        {
            nextGunInput = false;
            prevGunInput = false;
            return;
        }

        if (nextGunInput)
        {
            currentGunIndex++;
            nextGunInput = false;
            EquipGun(nextGun);
            return;
        }

        if (prevGunInput)
        {
            currentGunIndex--;
            prevGunInput = false;
            EquipGun(prevGun);
            return;
        }
    }

    public void EquipGun(Gun gunToEquip)
    {
        if (equippedGun)
        {
            Destroy(equippedGun.gameObject);
        }

        equippedGun = Instantiate(gunToEquip, weaponHoldTransform.position, weaponHoldTransform.rotation) as Gun;
        equippedGun.transform.parent = weaponHoldTransform;
        UpdateGunIndex();
    }

    void UpdateGunIndex()
    {
        //Update current gun index
        if (currentGunIndex < 0)
        {
            currentGunIndex = gunList.Count - 1;
        } else if (currentGunIndex == gunList.Count)
        {
            currentGunIndex = 0;
        }
        
        int nextGunIndex = currentGunIndex + 1;
        int prevGunIndex = currentGunIndex - 1;

        //Set next gun index
        if (nextGunIndex  == gunList.Count)
        {
            // If current gun is the last in the list
            nextGunIndex = 0;
        }

        //Set prev gun index
        if (prevGunIndex < 0)
        {
            // If current gun is the first in the list
            prevGunIndex = gunList.Count - 1;
        }



        Debug.Log("Next Gun Index: " + nextGunIndex);
        Debug.Log("Prev Gun Index: " + prevGunIndex);
        //Set gun obj
        nextGun = gunList[nextGunIndex];
        prevGun = gunList[prevGunIndex];

        //Update UI
        currentGunImage.sprite = equippedGun.gunSprite;
        nextGunImage.sprite = nextGun.gunSprite;
        prevGunImage.sprite = prevGun.gunSprite;
    }
}
