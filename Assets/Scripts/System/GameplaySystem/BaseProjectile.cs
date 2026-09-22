using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 必须要继承I(Fixed)Update1<DeployerContext>
/// 触发哪些物理回调自己写，无需写Update和FixedUpdate！！！
/// </summary>
public abstract class BaseProjectile : MonoBehaviour, ICanMoveToPool
{
    /// <summary>
    /// 代表发射时候的方向
    /// </summary>
    protected Vector3 direction;

    protected DeployerContext context;

    /// <summary>
    /// 正在控制的子弹的位置
    /// </summary>
    public Transform catchedTransform;

    /// <summary>
    /// 正在控制的子弹的碰撞体
    /// </summary>
    public Collider2D projectileCollider;

    /// <summary>
    /// 正在控制的子弹的刚体
    /// </summary>
    public Rigidbody2D projectileRigidbody;

    /// <summary>
    /// 发射它的发射器所在的物体
    /// </summary>
    protected GameObject deployerObject;

    protected void Update()
    {
        if (this is IUpdate1<DeployerContext> update)
        {
            update.IUpdate(context);
        }
    }

    protected void FixedUpdate()
    {
        if (this is IFixedUpdate1<DeployerContext> fixedUpdate)
        {
            fixedUpdate.IFixedUpdate(context);
        }
    }

    public void InitData(GameObject deployerObject, Vector3 direction, ref DeployerContext context)
    {
        this.deployerObject = deployerObject;
        this.direction = direction;
        this.context = context;
    }

    public void InitLocation(Vector3 position, Quaternion quaternion)
    {
        this.catchedTransform.position = position;
        this.catchedTransform.rotation = quaternion;
    }

    /// <summary>
    /// 在每次生成的时候触发一次
    /// </summary>
    public abstract void ActWhenAwake();
    public abstract void Enable();
    public abstract void Disable();
}
