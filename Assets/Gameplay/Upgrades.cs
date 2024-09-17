using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Upgrades : MonoBehaviour
{
    private int _dashForwardLevel = 0;
    private int _dashForwardMaxLevel = 10;
    private int _cooldownDashForwarlevel = 0;
    private int _cooldownDashForwardMaxLevel = 10;


    [SerializeField] private WheelController _wheelController;
    [SerializeField] private MoneyManager _moneyManager;

    [SerializeField] private TextMeshProUGUI _cooldownDashForwardLevelText;
    [SerializeField] private TextMeshProUGUI _dashForwardLevelText;

    [SerializeField] private GameObject _dashForwardMaxLevelWarningText;
    [SerializeField] private GameObject _cooldownDashForwardMaxLevelWarningText;
    public int DashForwardLevel => _dashForwardLevel;

    public void UpgradeDashForward()
    {
        if (_dashForwardLevel == _dashForwardMaxLevel)
        {
            return;
        }
        if (_moneyManager.AllMoney < _moneyManager.NeedMoneyForUpgradeDashForward)
        {
            return;
        }

        _moneyManager.AllMoney -= _moneyManager.NeedMoneyForUpgradeDashForward;
        float multiplier = 1.3f;
        _moneyManager.NeedMoneyForUpgradeDashForward = _moneyManager.MultiplyPrice(_moneyManager.NeedMoneyForUpgradeDashForward, multiplier);
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
        if (_cooldownDashForwarlevel == _cooldownDashForwardMaxLevel)
        {
            return;
        }
        if (_moneyManager.AllMoney < _moneyManager.NeedMoneyForUpgradeCooldownDashForward)
        {
            return;
        }

        _moneyManager.AllMoney -= _moneyManager.NeedMoneyForUpgradeCooldownDashForward;
        float multiplier = 1.3f;
        _moneyManager.NeedMoneyForUpgradeCooldownDashForward = _moneyManager.MultiplyPrice(_moneyManager.NeedMoneyForUpgradeCooldownDashForward, multiplier);
        _moneyManager.UpdateUi();
        _cooldownDashForwarlevel++;
        _wheelController.CooldownDashForward -= 1;
        _cooldownDashForwardLevelText.text = _cooldownDashForwarlevel.ToString();

        if (_cooldownDashForwarlevel == _cooldownDashForwardMaxLevel)
        {
            _cooldownDashForwardMaxLevelWarningText.SetActive(true);
            return;
        }
    }

    public void UpgradeIncome()
    {

    }
}

