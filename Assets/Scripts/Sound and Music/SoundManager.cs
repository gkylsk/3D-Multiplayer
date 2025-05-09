using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    GameManager gameManager;

    private static AudioSource audioSource;
    private static SoundEffectLibrary soundEffectLibrary;

    public float volume;
    public bool music;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            soundEffectLibrary = GetComponent<SoundEffectLibrary>();
            gameManager = GameManager.Instance;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void Play(string name)
    {
        AudioClip clip = soundEffectLibrary.GetAudioClip(name);
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, 2f);
        }
    }
    public void SetVolume()
    {
        audioSource.volume = volume;
        gameManager.SavePlayerData();
    }

    public void PlayMusic()
    {
        audioSource.Play();
        music = true;
        gameManager.SavePlayerData();
    }
    public void StopMusic()
    {
        audioSource.Stop();
        music = false;
        gameManager.SavePlayerData();
    }
}
