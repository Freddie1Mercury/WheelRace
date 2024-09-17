using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private float _allMoney;
    private float _moneyFromLastSession;
    private float _needMoneyForUpgradeIncome = 100;
    private float _needMoneyForUpgradeDashForward = 100;
    private float _needMoneyForUpgradeCooldownDashForward = 100;

    public float NeedMoneyForUpgradeIncome { get => _needMoneyForUpgradeIncome; set => _needMoneyForUpgradeIncome = value < 0 ? 0 : value; }
    public float NeedMoneyForUpgradeDashForward { get => _needMoneyForUpgradeDashForward; set => _needMoneyForUpgradeDashForward = value < 0 ? 0 : value; }
    public float NeedMoneyForUpgradeCooldownDashForward { get => _needMoneyForUpgradeCooldownDashForward; set => _needMoneyForUpgradeCooldownDashForward = value < 0 ? 0 : value; }
    public float AllMoney { get => _allMoney; set => _allMoney = value < 0 ? 0 : value; }

    [SerializeField] private TextMeshProUGUI _allMoneyText;
    [SerializeField] private TextMeshProUGUI _moneyFromLastSessionText;

    [SerializeField] private DistanceCounter _distanceCounter;


    public void AddMoneyForLastSession()
    {
        _moneyFromLastSession = (int)_distanceCounter.OldResult * 100;
        _allMoney += _moneyFromLastSession;
        UpdateUi();
        _moneyFromLastSession = 0;
    }

    public void ResetUI()
    {
        _moneyFromLastSessionText.text = _moneyFromLastSession.ToString();
    }

    public void UpdateUi()
    {
        _allMoneyText.text = _allMoney.ToString();
        _moneyFromLastSessionText.text = _moneyFromLastSession.ToString();
    }

    public float MultiplyPrice(float currentPrice, float multiplier)
    {
        return currentPrice * multiplier;
    }
}
