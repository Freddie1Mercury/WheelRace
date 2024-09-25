using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Upgrades : MonoBehaviour
{
    private int _dashForwardLevel = 0;
    private int _cooldownDashForwadrlevel = 0;
    private int _incomeLevel = 0;
    private int _startForceLevel = 0;

    private int _dashForwardMaxLevel = 10;
    private int _incomeMaxLevel = 10;
    private int _cooldownDashForwardMaxLevel = 10;
    private int _startForceMaxLevel = 10;


    [SerializeField] private WheelController _wheelController;
    [SerializeField] private MoneyManager _moneyManager;

    [SerializeField] private TextMeshProUGUI _cooldownDashForwardLevelText;
    [SerializeField] private TextMeshProUGUI _dashForwardLevelText;
    [SerializeField] private TextMeshProUGUI _incomeLevelText;
    [SerializeField] private TextMeshProUGUI _startForceLevelText;

    [SerializeField] private GameObject _dashForwardMaxLevelWarningText;
    [SerializeField] private GameObject _cooldownDashForwardMaxLevelWarningText;
    [SerializeField] private GameObject _incomeMaxLevelWarningText;
    [SerializeField] private GameObject _startForceMaxLevelWarningText;
    public int DashForwardLevel => _dashForwardLevel;

    public void UpgradeDashForward()
    {
        if (_dashForwardLevel == _dashForwardMaxLevel)
        {
            return;
        }
        if (_moneyManager.AllMoney < _moneyManager.UpgradeDashForwardPrice)
        {
            return;
        }

        _moneyManager.AllMoney -= _moneyManager.UpgradeDashForwardPrice;
        float multiplier = 1.3f;
        _moneyManager.UpgradeDashForwardPrice = _moneyManager.MultiplyPrice(_moneyManager.UpgradeDashForwardPrice, multiplier);
        _moneyManager.UpdateUi();
        _dashForwardLevel++;
        _dashForwardLevelText.text = _dashForwardLevel.ToString();
        _wheelController.DashForwardForce = _dashForwardLevel * 10;

        if (_dashForwardLevel == _dashForwardMaxLevel)
        {
            _dashForwardMaxLevelWarningText.SetActive(true);
            return;
        }
    }

    public void UpgradeCooldownDashForward()
    {
        if (_cooldownDashForwadrlevel == _cooldownDashForwardMaxLevel)
        {
            return;
        }
        if (_moneyManager.AllMoney < _moneyManager.UpgradeCooldownDashForwardPrice)
        {
            return;
        }

        _moneyManager.AllMoney -= _moneyManager.UpgradeCooldownDashForwardPrice;
        float multiplier = 1.3f;
        _moneyManager.UpgradeCooldownDashForwardPrice = _moneyManager.MultiplyPrice(_moneyManager.UpgradeCooldownDashForwardPrice, multiplier);
        _moneyManager.UpdateUi();
        _cooldownDashForwadrlevel++;
        _wheelController.CooldownDashForward -= 1;
        _cooldownDashForwardLevelText.text = _cooldownDashForwadrlevel.ToString();

        if (_cooldownDashForwadrlevel == _cooldownDashForwardMaxLevel)
        {
            _cooldownDashForwardMaxLevelWarningText.SetActive(true);
            return;
        }
    }

    public void UpgradeIncome()
    {
        if (_incomeLevel == _incomeMaxLevel)
        {
            return;
        }
        if (_moneyManager.AllMoney < _moneyManager.UpgradeIncomePrice)
        {
            return;
        }
        _incomeLevel += 1;
        _incomeLevelText.text = _incomeLevel.ToString();
        _moneyManager.AllMoney -= _moneyManager.UpgradeIncomePrice;
        float multiplier = 1.5f;
        _moneyManager.UpgradeIncomePrice = _moneyManager.MultiplyPrice(_moneyManager.UpgradeIncomePrice, multiplier);
        _moneyManager.MoneyMultipier += 1;
        _moneyManager.UpdateUi();

        if (_incomeLevel == _incomeMaxLevel)
        {
            _incomeMaxLevelWarningText.SetActive(true);
        }
    }

    public void UpgradeStartForce()
    {
        if (_startForceLevel == _startForceMaxLevel)
        {
            return;
        }
        if (_moneyManager.AllMoney < _moneyManager.UpgradeStartForcePrice)
        {
            return;
        }

        _startForceLevel += 1;
        _startForceLevelText.text = _startForceLevel.ToString();
        _moneyManager.AllMoney -= _moneyManager.UpgradeStartForcePrice;
        float multiplier = 1.5f;
        _moneyManager.UpgradeStartForcePrice = _moneyManager.MultiplyPrice(_moneyManager.UpgradeStartForcePrice, multiplier);
        _wheelController.StartForce += 10;
        _moneyManager.UpdateUi();

        if (_startForceLevel == _startForceMaxLevel)
        {
            _startForceMaxLevelWarningText.SetActive(true);
            return;
        }

    }

   
}

