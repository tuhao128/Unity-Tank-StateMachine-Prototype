using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 代表每次会发给炮弹的，这个发射器的信息，可以加入每个发射器的自己的特点
/// </summary>
public struct DeployerContext
{
    public Dictionary<string, float> deployerDetails;

    public Vector3 aimPos;

    public void InitDetails(Dictionary<string, float> deployerDetails)
    {
        this.deployerDetails = deployerDetails;
    }

    public void InitAimPos(Vector3 aimPos)
    {
        this.aimPos = aimPos;
    } 
}

/// <summary>
/// 使用它的时候作为一个物体存在，Update什么的Unity消息别写！！！
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BaseDeployer : MonoBehaviour, ICanMoveToPool
{
    public abstract string BaseName { get; }
    /// <summary>
    /// 存储自身的Rigidbody
    /// </summary>
    [SerializeField]
    protected Rigidbody2D selfRigidbody;

    /// <summary>
    /// 反作用于哪个Rigidbody
    /// </summary>
    public Rigidbody2D SelfRigidbody
    {
        set {  selfRigidbody = value; }
    }

    public bool isDebug;

    protected bool canLaunch;

    public bool CanLaunch {  get { return canLaunch; } }

    /// <summary>
    /// 代表了移动的速度
    /// </summary>
    [SerializeField]
    protected float speed;

    /// <summary>
    /// 这里存入发射的炮弹的预制体
    /// </summary>
    [SerializeField]
    protected GameObject projectilePrefab;

    [SerializeField]
    protected Transform catchedTransformPos;

    public Dictionary<string, float> deployerDetails = new Dictionary<string, float>();


    private void Start()
    {
        InitDeployer();
    }

    public void LaunchDirectly()
    {
        if (isDebug)
        {
            Launch(catchedTransformPos.right, speed);
        }
    }

    public void Launch(Vector3 direction)
    {
        Launch(direction, speed);
    }

    public void Launch(Vector2 direction)
    {
        Launch(direction, speed);
    }

    protected void Launch(Vector3 direction, float speed)
    {
        DeployerContext context = InitContext(deployerDetails);
        BaseProjectile projectile1;
        projectile1 = Generate(CreateIfPoolNull);
        Launch(projectile1, direction, speed);
        projectile1.InitData(this.gameObject, direction, ref context);
        projectile1.ActWhenAwake();
        ActWhenAwake(projectile1, direction);
        ReTime();
        canLaunch = false;
    }

    protected abstract void Launch(BaseProjectile projectile, Vector3 direction, float speed);

    protected abstract DeployerContext InitContext(Dictionary<string, float> deployerDetails);

    /// <summary>
    /// 持续作用于一个物体可以调用它
    /// </summary>
    /// <param name="projectile"></param>
    public abstract void ConstantInfluencedOn(BaseProjectile projectile);

    /// <summary>
    /// 每个fixedupdate都会调用一次，提示为true就记录然后不再调用它
    /// </summary>
    /// <returns></returns>
    protected abstract bool IsTimeOrUpdateTime();

    private void FixedUpdate()
    {
        if (!canLaunch)
        {
            canLaunch = IsTimeOrUpdateTime();
        }
    }

    /// <summary>
    /// 发射之后调用一次
    /// </summary>
    protected abstract void ReTime();

    /// <summary>
    /// 在每次生成物体之后触发一次
    /// </summary>
    protected abstract void ActWhenAwake(BaseProjectile projectile, Vector3 direction);

    /// <summary>
    /// Start末尾调用
    /// </summary>
    protected abstract void InitDeployer();

    public abstract void Disable();
    public abstract void Enable();

    protected BaseProjectile CreateIfPoolNull()
    {
        GameObject projectile = Instantiate(projectilePrefab, catchedTransformPos.position, catchedTransformPos.rotation);
        return projectile.GetComponent<BaseProjectile>();
    }

    /// <summary>
    /// 尝试获取已有的对象池中的对象
    /// </summary>
    /// <param name="projectile"></param>
    /// <param name="createIfNull"></param>
    /// <returns></returns>
    protected abstract BaseProjectile Generate(Func<BaseProjectile> createIfNull);

    /// <summary>
    /// 对象池取出后初始化位置和active状态
    /// </summary>
    /// <param name="projectile"></param>
    protected void InitAfterGenerate(BaseProjectile projectile)
    {
        projectile.InitLocation(catchedTransformPos.position, catchedTransformPos.rotation);
        projectile.gameObject.SetActive(true);
    }
}
