using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBPChangePanel : UIAbstractOnButtonPressed
{

    [SerializeField] BaseUIPanel panelToClose = null;
    [SerializeField] BaseUIPanel panelToOpen = null;

    protected override void OnButtonPressed()
    {
        UIManager.Instance.ClosePanel(panelToClose);
        UIManager.Instance.OpenPanel(panelToOpen);
    }
}
