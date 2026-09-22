using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonDeployer : BaseDeployer
{
    [SerializeField]
    private float force;

    [SerializeField]
    private float time;

    private float actualTime;

    public override string BaseName => "Cannon";

    public override void ConstantInfluencedOn(BaseProjectile projectile)
    {
        
    }

    public override void Disable()
    {
        this.gameObject.SetActive(false);
    }

    public override void Enable()
    {
        this.gameObject.SetActive(true);
    }

    protected override BaseProjectile Generate(Func<BaseProjectile> createIfNull)
    {
        BombProjectile bombProjectile = null;
        if (ObjectPool<BombProjectile>.GetFromPool(out bombProjectile, CreateIfPoolNull))
        {
            InitAfterGenerate(bombProjectile);
        }
        return bombProjectile;
    }

    protected override void ActWhenAwake(BaseProjectile projectile, Vector3 d)
    {
        selfRigidbody.AddForceAtPosition(-d * force, projectile.catchedTransform.position, ForceMode2D.Impulse);
    }

    protected override DeployerContext InitContext(Dictionary<string, float> deployerDetails)
    {
        DeployerContext deployerContext = new DeployerContext();
        deployerContext.deployerDetails = deployerDetails;
        return deployerContext;
    }

    protected override void InitDeployer()
    {
        actualTime = time;
    }

    protected override bool IsTimeOrUpdateTime()
    {
        if (actualTime > 0)
        {
            actualTime -= Time.fixedDeltaTime;
        }
        if (actualTime < 0)
        {
            actualTime = 0;
        }
        return actualTime <= 0;
    }

    protected override void Launch(BaseProjectile projectile, Vector3 direction, float speed)
    {
        
    }

    protected override void ReTime()
    {
        actualTime = time;
    }
}
