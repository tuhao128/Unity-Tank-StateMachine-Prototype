using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    public void ChangeAudioVolume(float value)
    {
        float volumeDB = Mathf.Log10(value) * 20f;
        audioMixer.SetFloat("AudioVolume", volumeDB);
    }

    public void ChangeMusicVolume(float value)
    {
        float volumeDB = Mathf.Log10(value) * 20f;
        audioMixer.SetFloat("MusicVolume", volumeDB);
    }
}
