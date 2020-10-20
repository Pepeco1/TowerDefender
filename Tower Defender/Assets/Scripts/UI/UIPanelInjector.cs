using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelInjector : MonoBehaviour
{

    [SerializeField] private List<BaseUIPanel> scenePanels = null;
    [SerializeField] private UIManager uiManagerPrefab = null;
    private UIManager uiManager = null;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();

        if(uiManager == null)
        {
            uiManager = Instantiate(uiManagerPrefab);
        }

        uiManager.InjectPanels(scenePanels);

        AttachCanvasToUIManager();
    }

    private void AttachCanvasToUIManager()
    {
        Canvas canvas = GetComponent<Canvas>();

        if (canvas == null)
        {
            Debug.LogWarning("[UIPanelInjector] PanelInjector is not on canvas");
            return;
        }

        canvas.transform.SetParent(uiManager.transform);
    }

    private void OnDestroy()
    {
        if (uiManager == null)
            Debug.LogError("[UIPanelInjector] PanelInjector with no reference to Manager");

        uiManager.RemovePanels(scenePanels);
    }

}
