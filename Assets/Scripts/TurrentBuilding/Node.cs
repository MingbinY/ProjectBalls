using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color originalColor;
    Renderer rend;

    private GameObject turret;


    private void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    #region hover effect
    private void OnMouseEnter()
    {
        rend.material.color = hoverColor;
        BuildManager.instance.selectedNode = this;
    }

    private void OnMouseExit()
    {
        rend.material.color = originalColor;
        BuildManager.instance.ResetSelectedNode(gameObject);
    }
    #endregion

    #region Build
    public void BuildTurret()
    {
        if (turret != null)
        {
            Debug.Log("This node has a turret");
        }
        else
        {
            // can build turrent
            GameObject turrentToBuild = BuildManager.instance.GetTurretToBuild();
            turret = Instantiate(turrentToBuild, transform.position + Vector3.up * 0.5f, Quaternion.identity) as GameObject;
        }
    }
    #endregion
}
