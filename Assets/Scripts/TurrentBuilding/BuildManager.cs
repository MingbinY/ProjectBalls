using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public Node selectedNode;
    public GameObject[] turretList;
    public GameObject turretSelected;
    public int currentIndex;

    public TurretUI[] turretUI;

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

        InitTurretUI();
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

            MoneyManager.instance.currentMoney -= turretToBuild.GetComponent<TurretAI>().config.buildCost;
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
            turretSelected = turretList[currentIndex];
            
        }
        else
        {
            currentIndex = indexToChange;
            // if it is different index, change to that index
            turretSelected = turretList[indexToChange];
        }

        UpdateTurretUI();
    }

    public void UpdateTurretUI()
    {
        TurretUI tUI = turretUI[currentIndex];
        TurretAI tAI = turretSelected.GetComponent<TurretAI>();
        tUI.turretIcon.sprite = tAI.config.turretIcon;
        tUI.turretCost.text = tAI.config.buildCost.ToString();
        UpdateSelectionIndicator();
        
    }

    public void UpdateSelectionIndicator()
    {
        for (int i = 0; i < turretList.Length; i++)
        {
            TurretUI tUI = turretUI[i];
            if (i == currentIndex)
            {
                tUI.selectedIndicator.enabled = true;
            }
            else
            {
                tUI.selectedIndicator.enabled = false;
            }
        }
    }

    public void InitTurretUI()
    {
        for (int i = 0; i < turretList.Length; i++)
        {
            TurretUI tUI = turretUI[i];
            TurretAI tAI = turretList[i].GetComponent<TurretAI>();

            tUI.turretIcon.sprite = tAI.config.turretIcon;
            tUI.turretCost.text = tAI.config.buildCost.ToString();
        }

        UpdateSelectionIndicator();
    }

    [System.Serializable]
    public class TurretUI
    {
        public Image turretIcon;
        public Text turretCost;
        public Image selectedIndicator;
    }
}
