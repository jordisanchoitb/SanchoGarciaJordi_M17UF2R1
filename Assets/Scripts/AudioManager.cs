using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;
    [SerializeField]
    private AudioSource mainSound, soundEffectExplosionGrenade, soundEffectShotRifle, soundEffectCollectCoin;
    void Start()
    {
        if (AudioManager.audioManager == null)
        {
            DontDestroyOnLoad(gameObject);
            AudioManager.audioManager = this;
            mainSound.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySoundEffectExplosionGrenade()
    {
        soundEffectExplosionGrenade.Play();
    }

    public void PlaySoundEffectShotRifle()
    {
        soundEffectShotRifle.Play();
    }

    public void PlaySoundEffectCollectCoin()
    {
        soundEffectCollectCoin.Play();
    }
}
