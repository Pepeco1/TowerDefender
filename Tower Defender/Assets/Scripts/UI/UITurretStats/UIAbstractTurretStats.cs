using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class UIAbstractTurretStats : MonoBehaviour
{

    private TMP_Text textComponent = null;

    protected string initialString = string.Empty;
    protected UITurretNode turretNode = null;

    private void Awake()
    {
        turretNode = GetComponentInParent<UITurretNode>();
        textComponent = GetComponentInChildren<TMP_Text>();
        initialString = textComponent.text;
    }

    private void OnEnable()
    {
        UpdateStats();
    }

    public void UpdateStats()
    {
        textComponent.SetText(GetTextWithStats());
    }

    public abstract string GetTextWithStats();
}
