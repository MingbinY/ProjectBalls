using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public Node selectedNode;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    public GameObject standardTurrentPrefab;

    private void Start()
    {
        turrentToBuild = standardTurrentPrefab;
    }

    public GameObject turrentToBuild;

    public GameObject GetTurrentToBuild()
    {
        return turrentToBuild;
    }
}
