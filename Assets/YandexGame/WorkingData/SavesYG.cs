
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 1;                       // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];

        // Ваши сохранения

        //MoneyManager Data
        public int UpgradeIncomePrice = 100;
        public int UpgradeDashForwardPrice = 100;
        public int UpgradeCooldownDashForwardPrice = 100;
        public int UpgradeStartForcePrice = 100;
        public float MoneyMultipier = 1;
        public float AllMoney = 400;

        //SkinShopCell Data
        public List<SkinSaveInfo> SkinSaveInfos = new List<SkinSaveInfo>(1);
        public int CountViewAds = 0;


        //Upgrades Data
        public int DashForwardLevel = 0;
        public int CooldownDashForwadrlevel = 0;
        public int IncomeLevel = 0;
        public int StartForceLevel = 0;

        //WheelController Data
        public int DashForwardForce = 20;
        public float CooldownDashForward = 15;
        public float RemainingTimeUntilDashForward = 0;
        public int StartForce = 30;

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива

            openLevels[1] = true;
        }
    }
}
