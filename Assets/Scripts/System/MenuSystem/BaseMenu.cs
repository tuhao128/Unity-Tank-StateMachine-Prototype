using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMenu : MonoBehaviour
{
    /// <summary>
    /// 注册时填写
    /// </summary>
    [SerializeField]
    private string actionName;

    [SerializeField]
    private GameObject menuRoot;

    /// <summary>
    /// 注册时填写
    /// </summary>
    public string ActionName { get { return actionName; } }

    /// <summary>
    /// 打开菜单
    /// </summary>
    public void Open()
    {
        this.menuRoot.SetActive(true);
        ActWhenOpen();
    }

    /// <summary>
    /// 关闭菜单
    /// </summary>
    public void Close()
    {
        ActWhenClose();
        this.menuRoot?.SetActive(false);
    }

    /// <summary>
    /// 菜单打开时激活，激活了根物体以后调用
    /// </summary>
    protected abstract void ActWhenOpen();

    /// <summary>
    /// 菜单关闭时激活，失活根物体之前调用
    /// </summary>
    protected abstract void ActWhenClose();

    /// <summary>
    /// 获取菜单的根物体，根物体用于统一激活和取消菜单的所有物体
    /// </summary>
    public GameObject MenuObjectRoot { get { return menuRoot; } }
}
