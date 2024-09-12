using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _backGroundMusic = new List<AudioClip>();
    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        StartCoroutine(PlayBackgroundMusic());
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
