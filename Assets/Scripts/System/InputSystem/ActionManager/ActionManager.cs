using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Profiling;

//这里用环形数组处理
public class ActionManager
{
    private int size;

    private CircularArray<ActionStack> unusedUpdates;

    private CircularArray<ActionStack> updateActions;

    private CircularArray<ActionStack> unusedFixedUpdates;

    private CircularArray<ActionStack> fixedUpdateActions;

    private static ActionManager instance;

    public static ActionManager Instance
    {
        get
        {
            return instance;
        }
    }

    public void AddUpdateAction(ActionStack action)
    {
        unusedUpdates.AddValue(action);
    }

    public void AddFixedUpdateAction(ActionStack action)
    {
        unusedFixedUpdates.AddValue(action);
    }

    //从缓冲区获取数据
    public void UpdateDatas()
    {
        (updateActions, unusedUpdates) = (unusedUpdates, updateActions);
    }

    //如果检测到当前还有东西没处理，就不执行读取
    public void FixedUpdateDatas()
    {
        if (fixedUpdateActions.IsEmpty)
        {
            (fixedUpdateActions, unusedFixedUpdates) = (unusedFixedUpdates, fixedUpdateActions);
        }
    }

    public void Update()
    {
        while (!updateActions.IsEmpty)
        {
            updateActions.ReadAndRemove().Update();
        }
        updateActions.Clear();
    }
    public void FixedUpdate()
    {
        while (!fixedUpdateActions.IsEmpty)
        {
            fixedUpdateActions.ReadAndRemove().FixedUpdate();
        }
        fixedUpdateActions.Clear();
    }

    public static void InitInstance(int size)
    {
        instance = new ActionManager();
        instance.size = size;
        instance.unusedUpdates = new CircularArray<ActionStack>(instance.size);
        instance.unusedFixedUpdates = new CircularArray<ActionStack>(instance.size);
        instance.updateActions = new CircularArray<ActionStack>(instance.size);
        instance.fixedUpdateActions = new CircularArray<ActionStack>(instance.size);
    }
}
