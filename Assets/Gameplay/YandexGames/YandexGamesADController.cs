using UnityEngine;
using YG;

public class YandexGamesADController : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;
    private void Start()
    {
        YandexGame.RewardVideoEvent += Rewarded;
        YandexGame.OpenFullAdEvent += () => _audioManager.OffAllSoundsAndMusic();
        YandexGame.CloseFullAdEvent += () => _audioManager.OnAllSoundsAndMusic();
        YandexGame.OpenVideoEvent += () => _audioManager.OffAllSoundsAndMusic();
        YandexGame.CloseVideoEvent += () => _audioManager.OnAllSoundsAndMusic();

         
    }
    private void Rewarded(int id)
    {
        if (id == 1)
        {
            //Выдача скина, либо его части
        }
    }
}
