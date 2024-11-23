using TMPro;
using UnityEngine;
using YG;

public class Upgrades : MonoBehaviour
{

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
    public int DashForwardLevel { get => YandexGame.savesData.DashForwardLevel; set => YandexGame.savesData.DashForwardLevel = value; }
    public int CooldownDashForwadrlevel { get => YandexGame.savesData.CooldownDashForwadrlevel; set => YandexGame.savesData.CooldownDashForwadrlevel = value; }
    public int IncomeLevel { get => YandexGame.savesData.IncomeLevel; set => YandexGame.savesData.IncomeLevel = value; }
    public int StartForceLevel { get => YandexGame.savesData.StartForceLevel; set => YandexGame.savesData.StartForceLevel = value; }

    private void Start()
    {
        UpdateUI();
    }

    public void UpgradeDashForward()
    {
        if (DashForwardLevel == _dashForwardMaxLevel)
        {
            return;
        }
        if (_moneyManager.AllMoney < _moneyManager.UpgradeDashForwardPrice)
        {
            return;
        }

        _moneyManager.DeductMoney(_moneyManager.UpgradeDashForwardPrice);
        float multiplier = 1.3f;
        _moneyManager.UpgradeDashForwardPrice = _moneyManager.MultiplyPrice(_moneyManager.UpgradeDashForwardPrice, multiplier);
        _moneyManager.UpdateUi();
        DashForwardLevel++;
        _dashForwardLevelText.text = DashForwardLevel.ToString();
        _wheelController.DashForwardForce = DashForwardLevel * 10;

        if (DashForwardLevel == _dashForwardMaxLevel)
        {
            _dashForwardMaxLevelWarningText.SetActive(true);
            
        }
        YandexGame.SaveProgress();
    }

    public void UpgradeCooldownDashForward()
    {
        if (CooldownDashForwadrlevel == _cooldownDashForwardMaxLevel)
        {
            return;
        }
        if (_moneyManager.AllMoney < _moneyManager.UpgradeCooldownDashForwardPrice)
        {
            return;
        }

        _moneyManager.DeductMoney(_moneyManager.UpgradeCooldownDashForwardPrice);
        float multiplier = 1.3f;
        _moneyManager.UpgradeCooldownDashForwardPrice = _moneyManager.MultiplyPrice(_moneyManager.UpgradeCooldownDashForwardPrice, multiplier);
        _moneyManager.UpdateUi();
        CooldownDashForwadrlevel++;
        _wheelController.CooldownDashForward -= 1;
        _cooldownDashForwardLevelText.text = CooldownDashForwadrlevel.ToString();

        if (CooldownDashForwadrlevel == _cooldownDashForwardMaxLevel)
        {
            _cooldownDashForwardMaxLevelWarningText.SetActive(true);
           
        }
        YandexGame.SaveProgress();
    }

    public void UpgradeIncome()
    {
        if (IncomeLevel == _incomeMaxLevel)
        {
            return;
        }
        if (_moneyManager.AllMoney < _moneyManager.UpgradeIncomePrice)
        {
            return;
        }
        IncomeLevel += 1;
        _incomeLevelText.text = IncomeLevel.ToString();
        _moneyManager.DeductMoney(_moneyManager.UpgradeIncomePrice);
        float multiplier = 1.5f;
        _moneyManager.UpgradeIncomePrice = _moneyManager.MultiplyPrice(_moneyManager.UpgradeIncomePrice, multiplier);
        _moneyManager.MoneyMultipier += 1;
        _moneyManager.UpdateUi();

        if (IncomeLevel == _incomeMaxLevel)
        {
            _incomeMaxLevelWarningText.SetActive(true);
        }
        YandexGame.SaveProgress();
    }

    public void UpgradeStartForce()
    {
        if (StartForceLevel == _startForceMaxLevel)
        {
            return;
        }
        if (_moneyManager.AllMoney < _moneyManager.UpgradeStartForcePrice)
        {
            return;
        }

        StartForceLevel += 1;
        _startForceLevelText.text = StartForceLevel.ToString();
        _moneyManager.DeductMoney(_moneyManager.UpgradeStartForcePrice);
        float multiplier = 1.5f;
        _moneyManager.UpgradeStartForcePrice = _moneyManager.MultiplyPrice(_moneyManager.UpgradeStartForcePrice, multiplier);
        _wheelController.StartForce += 10;
        _moneyManager.UpdateUi();

        if (StartForceLevel == _startForceMaxLevel)
        {
            _startForceMaxLevelWarningText.SetActive(true);
            
        }
        YandexGame.SaveProgress();

    }

    private void UpdateUI()
    {
        _cooldownDashForwardLevelText.text = CooldownDashForwadrlevel.ToString();
        _dashForwardLevelText.text = DashForwardLevel.ToString();
        _incomeLevelText.text = IncomeLevel.ToString();
        _startForceLevelText.text = StartForceLevel.ToString();

        if (DashForwardLevel == _dashForwardMaxLevel)
        {
            _dashForwardMaxLevelWarningText.SetActive(true);
            
        }
        if (CooldownDashForwadrlevel == _cooldownDashForwardMaxLevel)
        {
            _cooldownDashForwardMaxLevelWarningText.SetActive(true);
            
        }
        if (IncomeLevel == _incomeMaxLevel)
        {
            _incomeMaxLevelWarningText.SetActive(true);
        }
        if (StartForceLevel == _startForceMaxLevel)
        {
            _startForceMaxLevelWarningText.SetActive(true);
           
        }
    }


}

