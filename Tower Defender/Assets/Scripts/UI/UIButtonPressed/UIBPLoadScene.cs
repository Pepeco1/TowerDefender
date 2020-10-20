using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBPLoadScene : UIAbstractOnButtonPressed
{

    [SerializeField] private string sceneToLoad = string.Empty;
    [SerializeField] private BaseUIPanel panelToClose = null;

    protected override void OnButtonPressed()
    {
        if (HasSceneToLoad())
            GameManager.Instance.LoadSceneWithLoadingScreen(sceneToLoad);
        else
            Debug.LogError("[UIBPLoadScene] No scene to load");

        if (HasPanelToClose())
            UIManager.Instance.ClosePanel(panelToClose);
        else
            Debug.LogWarning("[UIBPLoadScene] No panel to close");
    }

    private bool HasPanelToClose()
    {
        return panelToClose != null;
    }

    private bool HasSceneToLoad()
    {
        return sceneToLoad != string.Empty;
    }
}
