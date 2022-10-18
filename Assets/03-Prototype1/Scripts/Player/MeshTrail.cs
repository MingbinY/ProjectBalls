using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTrail : MonoBehaviour
{
    [Header("Mesh")]
    private bool isActive;
    private MeshRenderer[] MeshRenderers;
    public Transform positionToSpawn;
    public float activeTime = 2f;
    public float meshRefreshRate = 0.1f;
    public float meshDestroyTimer = 1f;

    [Header("Shader")]
    public Material mat;


    private void Start()
    {
        positionToSpawn = transform;   
    }

    public void ShowTrail()
    {
        if (!isActive)
        {
            isActive = true;
            StartCoroutine(ActiveTrail(activeTime));
        }
    }

    IEnumerator ActiveTrail(float timeActive)
    {
        while (timeActive > 0)
        {
            timeActive -= meshRefreshRate;

            if (MeshRenderers == null)
                MeshRenderers = GetComponents<MeshRenderer>();

            for(int i = 0; i < MeshRenderers.Length; i++)
            {
                GameObject meshObj = new GameObject();
                meshObj.transform.SetPositionAndRotation(positionToSpawn.position, positionToSpawn.rotation);
                MeshRenderer mr = meshObj.AddComponent<MeshRenderer>();
                MeshFilter mf = meshObj.AddComponent<MeshFilter>();

                Mesh mesh = new Mesh();
                mf.mesh = mesh;
                mr.material = mat;
                Instantiate(meshObj, positionToSpawn);

                Destroy(meshObj, meshDestroyTimer);
            }

            yield return new WaitForSeconds(meshRefreshRate);
        }

        isActive = false;
    }
}
