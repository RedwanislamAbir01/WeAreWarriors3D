using _Game.Audios;
using _Game.Managers;
using _Tools.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private const float VOLUME = 1f;

    [SerializeField] public AudioClipRefsSO _audioClipRefsSO;
    [SerializeField] private AudioSource _inMusicAudioSource;
    [SerializeField] private AudioSource _playOneShotAudioSource;
    private void StartInGameMusic()
    {
        _inMusicAudioSource.Play();
    }
    private void Start()
    {
        GameManager.Instance.OnLevelStart += StartInGameMusic;
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnLevelStart -= StartInGameMusic;
    }
    public void StopInGameMusic()
    {
        _inMusicAudioSource.Stop();
    }

    public void PlaySound(AudioClip audioClip)
    {
        _playOneShotAudioSource.PlayOneShot(audioClip);
    }

}
