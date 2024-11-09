using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class YandexGamesADController : MonoBehaviour
{
    private void Start()
    {
        YandexGame.RewardVideoEvent += Rewarded;
    }
    private void Rewarded(int id)
    {
        if (id == 1)
        {
            //Выдача скина, либо его части
        }
    }
}
