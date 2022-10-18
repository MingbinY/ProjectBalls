using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color originalColor;
    Renderer rend;

    private GameObject turrent;


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
    }
    #endregion

    #region on click
    //private void OnMouseDown()
    //{
    //    if (turrent != null)
    //    {
    //        Debug.Log("This node has a turrent");
    //    }
    //    else
    //    {
    //        // can build turrent
    //        GameObject turrentToBuild = BuildManager.instance.GetTurrentToBuild();
    //        turrent = Instantiate(turrentToBuild, transform.position + Vector3.up * 0.5f, Quaternion.identity) as GameObject;
    //    }
    //}
    #endregion
}
