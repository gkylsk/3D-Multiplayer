using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectLibrary : MonoBehaviour
{
    [SerializeField] private SoundEffectGroup[] soundEffectGroups;
    private Dictionary<string, AudioClip> soundDictonary;

    private void Awake()
    {
        InitializeDictionary();
    }
    void InitializeDictionary()
    {
        soundDictonary = new Dictionary<string, AudioClip>();
        foreach (SoundEffectGroup sfg in soundEffectGroups)
        {
            soundDictonary[sfg.name] = sfg.audioClip;
        }
    }

    public AudioClip GetAudioClip(string name)
    {
        if (soundDictonary.ContainsKey(name))
        {
            AudioClip clip = soundDictonary[name];
            return clip;
        }
        return null;
    }
}

[System.Serializable]
public struct SoundEffectGroup
{
    public string name;
    public AudioClip audioClip;
}
