using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITurretsLayout : BaseUIPanel
{

    public int selectedIndex = 0;
    public UITurretNodeFlyWeight turretNodeFlyWeight = new UITurretNodeFlyWeight();
   
    [SerializeField] private UITurretNode[] turretNodesList = null;

    private UITurretNode currentSelected = null;

    private void Awake()
    {

        turretNodesList = GetComponentsInChildren<UITurretNode>();

        for (int i = 0; i < turretNodesList.Length; i++)
            turretNodesList[i].index = i;
    }

    private void Start()
    {
        //currentSelected = turretNodesList[0];
        //currentSelected.IsSelected = true;
    }


    public void SelectNode(UITurretNode turretNode)
    {

        if (currentSelected != null)
            currentSelected.IsSelected = false;

        turretNode.IsSelected = true;

        currentSelected = turretNode;

    }

}

[System.Serializable]
public class UITurretNodeFlyWeight
{
    public Color selectedColor;
    public Color unselectedColor;

}