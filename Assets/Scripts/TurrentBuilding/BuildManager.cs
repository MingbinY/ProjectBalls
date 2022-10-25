using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public Node selectedNode;
    public GameObject[] turretList;
    public GameObject turretSelected;
    public int currentIndex;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }  

    private void Start()
    {
        currentIndex = 0;
        turretSelected = turretList[currentIndex];
    }

    public GameObject GetTurretToBuild()
    {
        return turretSelected;
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
        GameObject turretToBuild = turretSelected;
        if (selectedNode != null)
        {
            //Check if selected node has turret
            if (selectedNode.hasTurret)
            {
                //Check if the turret is the same as selected turret
                if (turretToBuild.GetComponent<TurretAI>().type == selectedNode.turretType)
                {
                    // build next level
                    turretToBuild = selectedNode.turret.GetComponent<TurretAI>().nextLevel;
                    if (turretToBuild == null)
                    {
                        // Max Level
                        return;
                    }
                }
                else
                {
                    // if the selected turret is not the same type of the turret already on the node
                    // do nothing
                    return;
                }
            }
            // Check if has enough money to build or upgrade
            if (MoneyManager.instance.currentMoney < turretToBuild.GetComponent<TurretAI>().config.buildCost)
                return;
            selectedNode.BuildTurret(turretToBuild);
        }
    }

    public void ChangeSelectedIndex(int indexToChange)
    {
        if (indexToChange > turretList.Length || indexToChange <= 0)
            return;

        indexToChange--; //input 1 should be point to index 0
        if (indexToChange == currentIndex)
        {
            // If is the same index of selected turrent, change to its next level
            turretList[currentIndex] = turretList[currentIndex].GetComponent<TurretAI>().nextLevel;
        }
        else
        {
            // if it is different index, change to that index
            turretSelected = turretList[indexToChange];
        }
    }
}
