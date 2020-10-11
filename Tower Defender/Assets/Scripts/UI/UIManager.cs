using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : Singleton<UIManager>
{

    public UnityAction onLoadingScreenClose = null;
    public UnityAction onLoadingScreenOpen= null;

    private List<BaseUIPanel> allPanelsInScene = new List<BaseUIPanel>();
    private List<BaseUIPanel> activePanels = new List<BaseUIPanel>();
    private List<BaseUIPanel> temporarilyHiddenPanels = new List<BaseUIPanel>();
    private List<BaseUIPanel> popUpsPanels = new List<BaseUIPanel>();

    [SerializeField]
    private Camera m_DummyCamera;

    [SerializeField] private UILoadingScreen loadingScreen = null;

    private void OnEnable()
    {

        SubscribeToLoadingScreen();

    }


    private void OnDisable()
    {

        UnsubscribeToLoadingScreen();

    }


    public void OpenPanel(BaseUIPanel panel)
    {
        if (panel)
        {
            if (!PanelInAllPanelsList(panel))
            {
                InjectPanel(panel);
            }

            panel.OpenBehavior();
        }
    }

    public void ClosePanel(BaseUIPanel panel)
    {
        if (panel)
        {
            activePanels.Remove(panel);
            panel.CloseBehavior();
        }
    }

    public void OpenPopUp(BaseUIPanel panel)
    {
        if(panel && panel.panelType == PanelType.PopUp)
        {
            HideNonPopUps();
            popUpsPanels.Add(panel);
            OpenPanel(panel);
        }
    }

    public void InjectPanels(List<BaseUIPanel> panelsToInject)
    {
        foreach (var panel in panelsToInject)
        {
            InjectPanel(panel);
        }
    }

    public void RemovePanels(List<BaseUIPanel> panelsToRemove)
    {
        foreach (var panel in panelsToRemove)
        {
            RemovePanel(panel);
        }
    }

    public void HideGameHud()
    {
        foreach(var panel in activePanels)
        {
            if(panel.panelType == PanelType.HUD){
                HidePanel(panel);
            }
        }
    }

    public void SetDummyCamera(bool state)
    {
        m_DummyCamera.gameObject.SetActive(state);
    }

    public void LoadingScreen(AsyncOperation ao)
    {
        onLoadingScreenOpen?.Invoke();
        OpenPanel(loadingScreen);
        loadingScreen.LoadingSceneOperation = ao;
    }

    private void HidePanel(BaseUIPanel panel)
    {
        ClosePanel(panel);
        temporarilyHiddenPanels.Add(panel);
    }

    private void HideNonPopUps()
    {
        foreach(var panel in activePanels)
        {
            if(panel.panelType != PanelType.PopUp)
            {
                HidePanel(panel);
            }
        }
    }

    private void ReopenAllHiddenPanels()
    {
        foreach(var panel in temporarilyHiddenPanels)
        {
            panel.OpenBehavior();
        }

        temporarilyHiddenPanels.Clear();
    }

    private void InjectPanel(BaseUIPanel panel)
    {
        if (!PanelInAllPanelsList(panel))
        {
            allPanelsInScene.Add(panel);
            if (panel.isOpen)
                activePanels.Add(panel);
        }
    }

    private void RemovePanel(BaseUIPanel panel)
    {
        if(PanelInAllPanelsList(panel))
            allPanelsInScene.Remove(panel);
    }

    private bool PanelInAllPanelsList(BaseUIPanel panel)
    {
        return allPanelsInScene.Contains(panel);
    }

    private void loadingScreen_OnLoadingComplete()

    { 
        ClosePanel(loadingScreen);
        GameManager.Instance.GamePaused = false;
        onLoadingScreenClose?.Invoke();
    }

    private void UnsubscribeToLoadingScreen()
    {
        if (loadingScreen != null)
            loadingScreen.onLoadingComplete -= loadingScreen_OnLoadingComplete;
        else
            LogLoadingScreenWarning();
    }

    private void SubscribeToLoadingScreen()
    {
        if (loadingScreen != null)
            loadingScreen.onLoadingComplete += loadingScreen_OnLoadingComplete;
        else
            LogLoadingScreenWarning();
    }

    private static void LogLoadingScreenWarning()
    {
        Debug.LogWarning("[UIManager] Loading screen is Null");
    }

    private bool HasOnlyOneInstance()
    {
        return FindObjectsOfType<UIManager>().Length <= 1;
    }
}
