using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManager;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioSource loopAudioSource;
    [SerializeField] private AudioSource oneShotAudioSource;
    [SerializeField] private Slider volumeSlider;

    private static bool muted = false;

    void Start()
    {
        if(gameManager.CurrentLevel.LevelClip != null)
        {
            PlayLooping(gameManager.CurrentLevel.LevelClip);
        } else
        {
            PlayLooping(audioClip);
        }
        loopAudioSource.mute = muted;
        volumeSlider.value = AudioListener.volume;
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

    public void Mute()
    {
        muted = !loopAudioSource.mute;
        loopAudioSource.mute = muted;
        AudioListener.volume = muted ? 0f : 1f;
    }

    public void ControlVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }
}
