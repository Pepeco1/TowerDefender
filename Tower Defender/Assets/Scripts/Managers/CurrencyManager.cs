using UnityEngine;
using UnityEngine.Events;

public class CurrencyManager : Singleton<CurrencyManager>
{

    public bool canGainMoney = true;

    public float currentMoney = 0f;
    [SerializeField] private float maxMoney = 100f;

    [Header("Timepass Money Gain")]
    [SerializeField] private float moneyGainByTime = 0.5f;
    [SerializeField] private float deltaTimeToGainMoney = 0.25f;
    private float countToNextGain = 0;


    // UnityActions
    public UnityAction moneyChangeEvent;

    // Update is called once per frame
    void Update()
    {

        GainMoneyByTimePassage();

    }
    public bool ChangeMoney(float amount)
    {
        //if amount is negative and the player does not have enough money
        if((amount < 0f) && (currentMoney < Mathf.Abs(amount)))
        {
            return false;
        }

        currentMoney += amount;
        moneyChangeEvent.Invoke();

        return true;
    }

    private void GainMoneyByTimePassage()
    {

        if (canGainMoney)
        {
            countToNextGain += Time.deltaTime;
        }

        if (countToNextGain >= deltaTimeToGainMoney)
        {

            currentMoney += moneyGainByTime;
            moneyChangeEvent?.Invoke();
            countToNextGain = 0f;

        }
    }

}
