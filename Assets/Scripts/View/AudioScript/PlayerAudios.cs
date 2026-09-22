using System;
using System.Text.RegularExpressions;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAudios : MonoBehaviour
{
    [SerializeField]
    private float count_Time;

    private float m_Time;

    private bool flag;

    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private AudioClip[] movingClips;

    [SerializeField]
    private AudioClip jumpClip;

    [SerializeField]
    private AudioClip fireClip;

    private void Awake()
    {
        m_Time = count_Time;
    }

    public AudioClip RandomClip
    {
        get
        {
            int num = UnityEngine.Random.Range(0, movingClips.Length - 1);
            return movingClips[num];
        }
    }

    public void PlayMoving()
    {
        if (flag)
        {
            source.PlayOneShot(RandomClip);
            m_Time = count_Time;
            flag = false;
        }
    }

    public void PlayJump()
    {
        source.PlayOneShot(jumpClip);
    }

    public void PlayFire()
    {
        source.PlayOneShot(fireClip);
    }

    private void FixedUpdate()
    {
        if (m_Time > 0)
        {
            m_Time -= Time.fixedDeltaTime;
        }
        else
        {
            m_Time = 0;
            flag = true;
        }
    }
}
