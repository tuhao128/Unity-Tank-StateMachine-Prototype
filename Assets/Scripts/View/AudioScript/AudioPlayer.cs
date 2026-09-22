using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private static AudioPlayer instance;

    public static AudioPlayer Instance { get { return instance; } }

    private void Awake()
    {
        instance = this;
    }

    [SerializeField]
    private AudioSource m_AudioSource;

    [SerializeField]
    private Transform catchedTransform;

    public void PlaySoundAt(AudioClip clip, Vector3 position)
    {
        catchedTransform.position = position;
        m_AudioSource.PlayOneShot(clip);
    } 
}
