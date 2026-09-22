using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StateInformation
{
    public int intNum;

    public float floatNum;

    public bool boolNum;

    public double doubleNum;
}

public struct StateNode
{
    public string signedName;
    public BaseState state;
    public List<Transition> transitions;
    public StateInformation information;
    public List<StateNode> children;//----------------

    //in保护的是这个引用的内存地址
    public void Update(ref Context context)
    {
        if (state is IUpdate2<StateInformation, Context> update)
        {
            state.UpdateStateContext(ref context.stateContext);
            update.IUpdate(information, context);
        }
    }

    public void FixedUpdate(ref Context context)
    {
        if (state is IFixedUpdate2<StateInformation, Context> update)
        {
            state.UpdateStateContext(ref context.stateContext);
            update.IFixedUpdate(information, context);
        }
    }
}

public struct TransitionInformation
{
    public int intNum;

    public float floatNum;

    public bool boolNum;

    public double doubleNum;
}

public struct Transition
{
    public string signedName;
    public BaseTransition transition;
    public string toKey;
    public TransitionInformation information;
    public List<Premise> premises;

    public bool ShouldSwitch(in Condition condition, in StateInformation information)
    {
        return transition.ShouldSwitch(this.information, condition, information, premises);
    }

    public bool ShouldSwitch(in Context context, in StateInformation information)
    {
        return transition.ShouldSwitch(this.information, context, information, premises);
    }
}

//内部存储大量StateNode

//StateNode存储了当前执行的逻辑
//也写清楚了下一个链接的是哪个StateNode，链接触发的条件是什么

//每个StateNode都包含一个参数包
public class StateClip
{
    public Dictionary<string, StateNode> stateNodes = new Dictionary<string, StateNode>();

    private StateNode currentState;

    public StateNode CurrentState
    {
        get { return currentState; }
    }

    public void SetDefaultState(StateNode node)
    {
        this.currentState = node;
    }

    public void Update(ref Context context)
    {
        currentState.Update(ref context);
    }

    public void FixedUpdate(ref Context context)
    {
        currentState.FixedUpdate(ref context);
    }

    public void Switch(ref Condition condition)
    {
        if (currentState.transitions != null)
        {
            for (int i = 0; i < currentState.transitions.Count; i++)
            {
                if (currentState.transitions[i].ShouldSwitch(condition, currentState.information))
                {
                    currentState = stateNodes[currentState.transitions[i].toKey];
                    return;
                }
            }
        }
    }
    
    //这个会每帧进行切换

    public void Switch(ref Context context)
    {
        if (currentState.transitions != null)
        {
            for (int i = 0; i < currentState.transitions.Count; i++)
            {
                if (currentState.transitions[i].ShouldSwitch(context, currentState.information))
                {
                    currentState = stateNodes[currentState.transitions[i].toKey];
                    return;
                }
            }
        }
    }

    public void AddState(StateNode node)
    {
        stateNodes.Add(node.signedName + "_" + node.state.BaseName, node);
    }
}
