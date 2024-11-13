using TMPro;
using UnityEngine;
using YG;

public class MoneyManager : MonoBehaviour
{
    private float _moneyFromLastSession;

    public int UpgradeIncomePrice { get => YandexGame.savesData.UpgradeIncomePrice; set => YandexGame.savesData.UpgradeIncomePrice = value < 0 ? 0 : value; }
    public int UpgradeDashForwardPrice { get => YandexGame.savesData.UpgradeDashForwardPrice; set => YandexGame.savesData.UpgradeDashForwardPrice = value < 0 ? 0 : value; }
    public int UpgradeCooldownDashForwardPrice { get => YandexGame.savesData.UpgradeCooldownDashForwardPrice; set => YandexGame.savesData.UpgradeCooldownDashForwardPrice = value < 0 ? 0 : value; }
    public int UpgradeStartForcePrice { get => YandexGame.savesData.UpgradeStartForcePrice; set => YandexGame.savesData.UpgradeStartForcePrice = value < 0 ? 0 : value; }
    public float MoneyMultipier { get => YandexGame.savesData.MoneyMultipier; set => YandexGame.savesData.MoneyMultipier = value < 0 ? 0 : value; }
    public float AllMoney { get => YandexGame.savesData.AllMoney;}

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
        _moneyFromLastSession = (int)_distanceCounter.OldResult / 10 * YandexGame.savesData.MoneyMultipier;
        YandexGame.savesData.AllMoney += _moneyFromLastSession;
        UpdateUi();
        _moneyFromLastSession = 0;
    }

    public void ResetUI()
    {
        _moneyFromLastSessionText.text = _moneyFromLastSession.ToString();
    }

    public void UpdateUi()
    {
        _allMoneyText.text = AllMoney.ToString();
        _moneyFromLastSessionText.text = _moneyFromLastSession.ToString();
        _upgradeCooldownDashForwardPriceText.text = YandexGame.savesData.UpgradeCooldownDashForwardPrice.ToString();
        _upgradeDashForwardPriceText.text = YandexGame.savesData.UpgradeDashForwardPrice.ToString();
        _upgradeIncomePriceText.text = YandexGame.savesData.UpgradeIncomePrice.ToString();
        _upgradeStartForcePriceText.text = YandexGame.savesData.UpgradeStartForcePrice.ToString();
        _upgradeStartForcePriceText.text = YandexGame.savesData.UpgradeStartForcePrice.ToString();
    }

    public int MultiplyPrice(float currentPrice, float multiplier)
    {
        float temp = multiplier * currentPrice;
        return (int)temp;
    }

    public void AddMoney(int money)
    {
        if (money > 0)
        {
            YandexGame.savesData.AllMoney += money;
        }
        UpdateUi();
    }

    public void DeductMoney(int money)
    {
        if (money < YandexGame.savesData.AllMoney)
        {
            YandexGame.savesData.AllMoney -= money;
        }
        UpdateUi();
    }
}
