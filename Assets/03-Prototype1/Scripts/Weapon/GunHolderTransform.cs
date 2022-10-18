using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHolderTransform : MonoBehaviour
{
    public GameObject playerObj;
    float rotationAngle;
    public float rotationSpeed;
    Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        transform.position = playerObj.transform.position;
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            point.y = transform.position.y;
            transform.LookAt(point);
        }
    }
}
