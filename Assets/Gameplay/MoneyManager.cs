using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private float _allMoney = 100000;
    private float _moneyFromLastSession;
    private float _moneyMultipier = 1;

    private int _upgradeIncomePrice = 100;
    private int _upgradeDashForwardPrice = 100;
    private int _upgradeCooldownDashForwardPrice = 100;
    private int _upgradeStartForcePrice = 100;

    public float MoneyMultipier { get => _moneyMultipier; set => _moneyMultipier = value < 0 ? 0 : value; }
    public int UpgradeIncomePrice { get => _upgradeIncomePrice; set => _upgradeIncomePrice = value < 0 ? 0 : value; }
    public int UpgradeDashForwardPrice { get => _upgradeDashForwardPrice; set => _upgradeDashForwardPrice = value < 0 ? 0 : value; }
    public int UpgradeCooldownDashForwardPrice { get => _upgradeCooldownDashForwardPrice; set => _upgradeCooldownDashForwardPrice = value < 0 ? 0 : value; }
    public int UpgradeStartForcePrice { get => _upgradeStartForcePrice; set => _upgradeStartForcePrice = value < 0 ? 0 : value; }
    public float AllMoney { get => _allMoney; set => _allMoney = value < 0 ? 0 : value; }

    [SerializeField] private TextMeshProUGUI _allMoneyText;
    [SerializeField] private TextMeshProUGUI _moneyFromLastSessionText;

    [SerializeField] private TextMeshProUGUI _upgradeIncomePriceText;
    [SerializeField] private TextMeshProUGUI _upgradeDashForwardPriceText;
    [SerializeField] private TextMeshProUGUI _upgradeCooldownDashForwardPriceText;
    [SerializeField] private TextMeshProUGUI _upgradeStartForcePriceText;

    [SerializeField] private DistanceCounter _distanceCounter;

    private void Start()
    {
        UpdateUi();
    }

    public void AddMoneyForLastSession()
    {
        _moneyFromLastSession = (int)_distanceCounter.OldResult / 10 * _moneyMultipier;
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
        _upgradeCooldownDashForwardPriceText.text = _upgradeCooldownDashForwardPrice.ToString();
        _upgradeDashForwardPriceText.text = _upgradeDashForwardPrice.ToString();
        _upgradeIncomePriceText.text = _upgradeIncomePrice.ToString();
        _upgradeStartForcePriceText.text= _upgradeStartForcePrice.ToString();
    }

    public int MultiplyPrice(float currentPrice, float multiplier)
    {
        float temp = multiplier * currentPrice;
        return (int)temp;
    }
}
