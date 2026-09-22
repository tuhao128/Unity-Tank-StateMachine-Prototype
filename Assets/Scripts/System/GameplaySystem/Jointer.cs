using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JointerEventType
{
    None,
    LaunchEvent,
    SwitchEvent
}
public class Jointer : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D selfRigidbody;

    private Color defaultColor;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private MaterialPropertyBlock materialPropertyBlock;

    private event Action<BaseDeployer> launchEvent;

    private event Action<BaseDeployer> switchEvent;

    private Transform catchedTransform;

    [SerializeField]
    private BaseDeployer deployer;

    [SerializeField]
    private BaseDeployer preDeployer;

    private void Start()
    {
        this.catchedTransform = this.transform;
        if (preDeployer != null)
        {
            UpLoad(preDeployer.gameObject);
        }
        defaultColor = spriteRenderer.color;
    }

    /// <summary>
    /// 发射操作
    /// 条件符合时结尾触发一次事件
    /// </summary>
    public bool Launch()
    {
        if (deployer != null)
        {
            if (deployer.CanLaunch)
            {
                deployer.LaunchDirectly();
                launchEvent?.Invoke(deployer);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 实现武器装配
    /// 结尾触发一次事件
    /// </summary>
    public void UpLoad(GameObject deployer)
    {
        GameObject gameObject = Instantiate(deployer, catchedTransform);
        gameObject.TryGetComponent<BaseDeployer>(out this.deployer);
        this.deployer.SelfRigidbody = this.selfRigidbody;
        switchEvent?.Invoke(this.deployer);
    }

    public void Subscribe(JointerEventType type, Action<BaseDeployer> action)
    {
        switch (type)
        {
            case JointerEventType.None:
                break;
            case JointerEventType.LaunchEvent:
                launchEvent += action;
                break;
            case JointerEventType.SwitchEvent:
                switchEvent += action;
                break;
            default:
                break;
        }
    }

    public void Selected()
    {
        if (materialPropertyBlock == null)
        {
            materialPropertyBlock = new MaterialPropertyBlock();
        }
        spriteRenderer.GetPropertyBlock(materialPropertyBlock);
        materialPropertyBlock.SetColor("_Color", Color.blue);
        spriteRenderer.SetPropertyBlock(materialPropertyBlock);
    }

    public void Disselected()
    {
        if (materialPropertyBlock == null)
        {
            materialPropertyBlock = new MaterialPropertyBlock();
            spriteRenderer.GetPropertyBlock(materialPropertyBlock);
        }
        spriteRenderer.GetPropertyBlock(materialPropertyBlock);
        materialPropertyBlock.SetColor("_Color", Color.white);
        spriteRenderer.SetPropertyBlock(materialPropertyBlock);
    }
}
