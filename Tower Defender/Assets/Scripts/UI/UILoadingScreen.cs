using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UILoadingScreen : BaseUIPanel
{

    private AsyncOperation _loadingSceneOperation = null;
    public AsyncOperation LoadingSceneOperation
    {
        get
        {
            return _loadingSceneOperation;
        }

        set
        {
            _loadingSceneOperation = value;

            if (this.isOpen == false)
            {
                UIManager.Instance.OpenPanel(this);
            }
        }
    }

    [SerializeField] private Image progressBar = null;
    [SerializeField] private int closeDelayInSeconds = 3;
    private Animator myAnimator = null;

    public UnityAction onLoadingComplete = null; 

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HasSlider())
        {
            UpdateProgressBar();
        }

        if (IsLoadingFinished())
        {
            StartCoroutine(DelayedClose());
        }
    }


    public void Close()
    {
        _loadingSceneOperation = null;
        onLoadingComplete?.Invoke();
        base.CloseBehavior();
    }

    public override void OpenBehavior()
    {
        base.OpenBehavior();
    }

    private IEnumerator DelayedClose()
    {

        yield return new WaitForSecondsRealtime(closeDelayInSeconds);

        Close();

    }
    private bool IsLoadingFinished()
    {
        return _loadingSceneOperation.progress == 1;
    }

    private void UpdateProgressBar()
    {
        progressBar.fillAmount = _loadingSceneOperation.progress;
    }

    private bool HasSlider()
    {
        return progressBar != null;
    }
}
