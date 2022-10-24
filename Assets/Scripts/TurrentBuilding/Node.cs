using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color originalColor;
    Renderer rend;

    public GameObject turret;
    public bool hasTurret;
    public TurretType turretType;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
        hasTurret = false;
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
    public void BuildTurret(GameObject turretToBuild)
    {
        if (hasTurret)
        {
            hasTurret = false;
            Destroy(turret);
        }
        turret = Instantiate(turretToBuild, transform.position + Vector3.up * 0.9f, Quaternion.identity) as GameObject;
        turretType = turret.GetComponent<TurretAI>().type;
        hasTurret = true;
    }
    #endregion
}
