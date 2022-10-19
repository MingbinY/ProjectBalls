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
        turretToBuild = standardTurrentPrefab;
    }

    public GameObject turretToBuild;

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void ResetSelectedNode(GameObject passInNodeObject)
    {
        if (selectedNode.gameObject == passInNodeObject)
        {
            selectedNode = null;
        }
    }

    public void BuildTurret()
    {
        if (selectedNode != null)
        {
            selectedNode.BuildTurret();
        }
    }
}
