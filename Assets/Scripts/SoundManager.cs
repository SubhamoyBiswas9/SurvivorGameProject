using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    AudioSource audioSource;

    [SerializeField] AudioClip playerShootSFX;
    [SerializeField] AudioClip enemyShootSFX;
    [SerializeField] AudioClip enemyHitSFX;
    [SerializeField] AudioClip coinCollectSFX;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void PlayAudio(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    public void PLayerShootSFX()
    {
        PlayAudio(playerShootSFX);
    }

    public void EnemyShootSFX()
    {
        PlayAudio(enemyShootSFX);
    }

    public void EnemyHitSFX()
    {
        PlayAudio(enemyHitSFX);
    }

    public void PlayCoinCollectSFX()
    {
        PlayAudio(coinCollectSFX);
    }
}
