using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _backGroundMusic = new List<AudioClip>();
    [SerializeField] private  List<GameObject> _soundPlayers = new List<GameObject>(1);
    [SerializeField] private GameObject _soundPlayerPrefab;
    [SerializeField] private Transform _soundsPlayersPosition;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Slider _backGroundMusicSlider;
     



    private void Start()
    {
        StartCoroutine(PlayBackgroundMusic());
        CreateSoundsPlayers();
    }

    private void Update()
    {
        _audioSource.volume = _backGroundMusicSlider.value;
    }

    private IEnumerator PlayBackgroundMusic()
    {
        while (true)
        {
            foreach (var clip in _backGroundMusic)
            {
                _audioSource.clip = clip;
                _audioSource.Play();
                yield return new WaitForSeconds(clip.length);
            }
        }
    }

    private void CreateSoundsPlayers()
    {
        _soundPlayers.Add(Instantiate(_soundPlayerPrefab, _soundsPlayersPosition)); 
    }

    public void PlaySound(AudioClip audioClip,float volume = 0.1f, float pithc = 1)
    {
        int counterActiveSoundPlayer = 0;

        foreach (var soundPlayer in _soundPlayers)
        {
            if (soundPlayer.GetComponent<AudioSource>().isPlaying)
            {
                counterActiveSoundPlayer++;
            }
            if (counterActiveSoundPlayer == _soundPlayers.Count)
            {
                CreateSoundsPlayers();
                break;

            }
        }

        foreach(var soundPlayer in _soundPlayers)
        {
            if (!soundPlayer.GetComponent<AudioSource>().isPlaying)
            {
                soundPlayer.GetComponent<AudioSource>().clip = null;
            }
            if (soundPlayer.GetComponent<AudioSource>().clip == null)
            {
                soundPlayer.GetComponent<AudioSource>().clip = audioClip;
                soundPlayer.GetComponent<AudioSource>().volume = volume;
                soundPlayer.GetComponent<AudioSource>().pitch = pithc;
                soundPlayer.GetComponent <AudioSource>().Play();
                return;
            }
            if (soundPlayer.GetComponent<AudioSource>().isPlaying)
            {
                counterActiveSoundPlayer++;
            }
        }
    }
    public  void OffAllSoundsAndMusic()
    {
        _audioSource.Pause();

        foreach (var soundPlayer in _soundPlayers)
        {
            soundPlayer.GetComponent<AudioSource>().Pause();
        }
    }

    public void OnAllSoundsAndMusic()
    {
        _audioSource.UnPause();

        foreach (var soundPlayer in _soundPlayers)
        {
            soundPlayer.GetComponent<AudioSource>().UnPause();
        }
    }

}
