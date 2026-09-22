using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : BaseProjectile
{
    [SerializeField]
    private LayerMask incluedMask;

    [SerializeField]
    private AudioPlayer player;    

    [SerializeField]
    private AudioClip audioClip;

    [SerializeField]
    private float force;

    [SerializeField]
    private float inertia;

    public override void ActWhenAwake()
    {
        projectileRigidbody.AddForceAtPosition(inertia * direction, catchedTransform.position, ForceMode2D.Impulse);
    }

    public override void Disable()
    {
        this.gameObject.SetActive(false);
    }

    public override void Enable()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((1 << collision.gameObject.layer & incluedMask.value) != 0)
        {
            AudioPlayer.Instance.PlaySoundAt(audioClip, catchedTransform.position);
            //audioSource.Play();//会在物体失活之后再检查要不要播放
            ObjectPool<BombProjectile>.RemoveToPool(this);
        }
    }
}
