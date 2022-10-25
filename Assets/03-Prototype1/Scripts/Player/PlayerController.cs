using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    GunController gunController;
    AbilityHolder abilityHolder;
    Rigidbody vfxRb;
    Rigidbody rb;
    [SerializeField]
    float moveForce = 1f;
    float movementX, movementY;
    public int score = 0;
    public Vector3 movementVector;
    public int winScore = 30;

    private Transform cameraTransform;
    public float rotationSpeed = 5f;

    public bool jumpInput;
    public bool buildTurretInput;
    public bool reloadInput;
    public Player inputActions;
    float numberInput;
    bool numInputPerformed;

    void Awake()
    {
        score = 0;
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
        abilityHolder = GetComponent<AbilityHolder>();
        gunController = FindObjectOfType<GunController>();
        if (inputActions == null)
        {
            inputActions = new Player();
        }

        inputActions.Action.SwitchTurrent.performed += i => numberInput = i.ReadValue<float>();
        inputActions.Action.BuildTurrent.performed += i => buildTurretInput =true;
        inputActions.Enable();
    }

    public void OnMovement(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void HandleJump()
    {
        inputActions.Movement.Jump.performed += i => jumpInput = true;
        if (jumpInput)
        {
            Jump();
            jumpInput = false;
        }
    }

    public void HandleBuildTurretInput()
    {
        if (buildTurretInput)
        {
            Debug.Log("Build");
            BuildManager.instance.BuildTurret();
            buildTurretInput = false;
        }
            
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }

    private void Update()
    {
        //HandleJump();
        HandleSwitchTurretInput();
        HandleBuildTurretInput();
        HandleReloadInput();
    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0, movementY);
        movementVector = movement.normalized;
        rb.AddForce(movement * moveForce);
    }

    public void Win()
    {
        Destroy(gameObject);
        FindObjectOfType<InGameMenu>().Win();
    }

    void HandleSwitchTurretInput()
    {
        inputActions.Action.SwitchTurrent.performed += i => numInputPerformed = true;
        if (numInputPerformed)
        {
            Debug.Log(numberInput);
            numInputPerformed = false;
            BuildManager.instance.ChangeSelectedIndex((int)numberInput);
        }
    }

    void HandleReloadInput()
    {
        inputActions.Action.Reload.performed += i => reloadInput = true;
        if (reloadInput)
        {
            reloadInput = false;
            StartCoroutine(gunController.equippedGun.Reload());
        }
    }
}
