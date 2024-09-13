using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _backGroundMusic = new List<AudioClip>();
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Slider _backGroundMusicSlider;

    private void Start()
    {
        StartCoroutine(PlayBackgroundMusic());
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
}
