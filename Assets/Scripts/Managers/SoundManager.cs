using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManager;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioSource loopAudioSource;
    [SerializeField] private AudioSource oneShotAudioSource;
    
    void Start()
    {
        if(gameManager.CurrentLevel.LevelClip != null)
        {
            PlayLooping(gameManager.CurrentLevel.LevelClip);
        } else
        {
            PlayLooping(audioClip);
        }
        
    }

    public void PlayLooping(AudioClip audioClip) 
    {
        loopAudioSource.clip = audioClip;
        loopAudioSource.Play();
    }

    public void PlayOnce(AudioClip audioClip)
    {
        oneShotAudioSource.PlayOneShot(audioClip);
    }
}
