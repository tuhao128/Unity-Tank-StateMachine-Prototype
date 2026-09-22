using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private static MenuManager instance;

    public static MenuManager Instance {  get { return instance; } }

    [SerializeField]
    private BaseMenu[] menus;

    private Dictionary<string, BaseMenu> keyValuePairs = new Dictionary<string, BaseMenu>();

    private Stack<BaseMenu> openedMenuStack = new Stack<BaseMenu>();

    private void Awake()
    {
        instance = this;
        for (int i = 0; i < menus.Length; i++)
        {
            keyValuePairs.TryAdd(menus[i].ActionName, menus[i]);
            menus[i].MenuObjectRoot.SetActive(false);
        }
    }

    /// <summary>
    /// 填写Menu绑定inputAction名开启对应Menu
    /// </summary>
    public void Open(string inputActioName)
    {
        if (keyValuePairs.TryGetValue(inputActioName, out BaseMenu menu))
        {
            if (!openedMenuStack.Contains(menu))
            {
                menu.Open();
                openedMenuStack.Push(menu);
            }
            else
            {
                Close(menu);
            }
        }
    }

    /// <summary>
    /// 关闭最新打开的菜单
    /// </summary>
    public void Close()
    {
        if (openedMenuStack.Count != 0)
        {
            BaseMenu menu = openedMenuStack.Pop();
            menu.Close();
        }
        
    }

    /// <summary>
    /// 以某个menu为界限，挨个退出一直到这个menu退出
    /// </summary>
    /// <param name="menu"></param>
    public void Close(BaseMenu menu)
    {
        if (openedMenuStack.TryPop(out BaseMenu result))
        {
            result.Close();
            if (result == menu)
            {
                return;
            }
            else
            {
                Close(menu);
            }
        }
    }
}
