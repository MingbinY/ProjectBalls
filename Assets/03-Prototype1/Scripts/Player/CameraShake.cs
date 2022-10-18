using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class CameraShake : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    CameraMovement cameraMovement;
    Vector3 originalLocation;
    private void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraMovement = mainCamera.GetComponent<CameraMovement>();
        originalLocation = mainCamera.transform.localPosition;
    }

    public void ActiveShake(float duration, float magnitude)
    {
        //cameraMovement.isShaking = true;
        //StartCoroutine(ShakeEnumerator(duration, magnitude));
    }

    IEnumerator ShakeEnumerator(float duration, float magnitude)
    {
        originalLocation = mainCamera.transform.localPosition;
        Vector3 origin = originalLocation;

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, origin.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = origin;
    }
}
