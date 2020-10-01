using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITurretsLayout : BaseUIPanel
{

    public int selectedIndex = 0;
    public UITurretNodeFlyWeight turretNodeFlyWeight = new UITurretNodeFlyWeight();
   
    [SerializeField] private List<UITurretNode> turretNodesList = new List<UITurretNode>();

    private UITurretNode currentSelected = null;

    private void Awake()
    {
        for (int i = 0; i < turretNodesList.Count; i++)
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