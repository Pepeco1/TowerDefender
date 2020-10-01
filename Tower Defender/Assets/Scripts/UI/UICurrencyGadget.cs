using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class UICurrencyGadget : BaseUIPanel
{

    [SerializeField] private TextMeshProUGUI textMP = null;
    private bool currencyManagerIsEnabled;

    private void OnEnable()
    {
        CurrencyManager.Instance.moneyChangeEvent += UpdateCurrencyText;
    }

    private void OnDisable()
    {
        CurrencyManager.Instance.moneyChangeEvent -= UpdateCurrencyText;
    }

    public void UpdateCurrencyText()
    {
        textMP.SetText(String.Format("{0}", (int) CurrencyManager.Instance.currentMoney));
    }




}
