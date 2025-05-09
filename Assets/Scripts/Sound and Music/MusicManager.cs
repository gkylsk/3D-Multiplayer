using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    SoundManager soundManager;
    [SerializeField] Slider slider;

    [SerializeField] GameObject musicOn;
    [SerializeField] GameObject musicOff;

    void Start()
    {
        soundManager = SoundManager.Instance;
        slider.onValueChanged.AddListener(delegate { SetVolumeSlider(); });
        GameManager.Instance.LoadPlayerData();
        SetSlider();
        SetMusic();
    }

    public void SetVolumeSlider()
    {
        soundManager.volume = slider.value;
        soundManager.SetVolume();
    }

    public void SetSlider()
    {
        slider.value = soundManager.volume;
    }

    void SetMusic()
    {
        if (soundManager.music)
        {
            PlayMusic();
        }
        else
        {
            StopMusic();
        }
    }
    public void PlayMusic()
    {
        soundManager.PlayMusic();
        musicOff.SetActive(false);
        musicOn.SetActive(true);
    }
    public void StopMusic()
    {
        soundManager.StopMusic();
        musicOn.SetActive(true);
        musicOff.SetActive(false);
    }
}
