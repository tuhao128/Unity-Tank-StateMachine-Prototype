using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class StateClipFactory
{
    private static StateClipFactory instance;

    private string[] basicStates = new string[] { "Any", "OnEnter", "OnExit" };

    public static StateClipFactory Instance
    {
        get
        {
            return instance;
        }
    }

    public static void InitInstance()
    {
        instance = new StateClipFactory();
    }

    public StateClip CreateStateClip(StateClipConfig config)
    {
        StateClip clip = new StateClip();

        StateNode node = CreateStateNode(config.OnEnterConfig);
        if (config.OnEnterConfig.shouldDefault)
        {
            clip.SetDefaultState(node);
        }
        clip.AddState(node);

        node = CreateStateNode(config.OnExitConfig);
        clip.AddState(node);

        node = CreateStateNode(config.AnyConfig);
        clip.AddState(node);

        for (int i = 0; i < config.states.Length; i++)
        {
            if (IsBasic(config.states[i].baseName))
            {
#if UNITY_EDITOR
                Debug.LogWarning("错误添加了基础状态，基础状态无需手动添加");
#endif
                continue;
            }
            node = CreateStateNode(config.states[i]);
            if (config.states[i].shouldDefault)
            {
                clip.SetDefaultState(node);
            }
            clip.AddState(node);
        }
        return clip;
    }

    public StateNode CreateStateNode(StateConfig config)
    {
        StateNode node = new StateNode();
        node.state = StateRegisters.Instance.GetState(config.baseName);
        node.signedName = config.signedName;
        node.information = CreateStateInformation(config.informationConfig);
        if (config.transitionConfigs != null)
        {
            node.transitions = new List<Transition>(config.transitionConfigs.Length);
            for (int i = 0; i < config.transitionConfigs.Length; i++)
            {
                node.transitions.Add(CreateTransition(config.transitionConfigs[i]));
            }
        }
        return node;
    }

    public StateInformation CreateStateInformation(StateInforConfig config)
    {
        StateInformation information = new StateInformation();
        if (config != null)
        {
            information.boolNum = config.boolNum;
            information.doubleNum = config.doubleNum;
            information.floatNum = config.floatNum;
            information.intNum = config.intNum;
        }
        
        return information;
    }

    public Transition CreateTransition(TransitionConfig config)
    {
        Transition transition = new Transition();
        transition.signedName = config.signedName;
        transition.toKey = config.toSignedName + "_" + config.toBaseName;
        transition.transition = TransitionRegisters.Instance.GetTransition(config.baseName);
        transition.information = CreateTransitionInformation(config.inforConfig);
        if (config.premiseConfigs != null)
        {
            transition.premises = new List<Premise>(config.premiseConfigs.Length);
            for (int i = 0; i < config.premiseConfigs.Length; i++)
            {
                transition.premises.Add(CreatePremise(config.premiseConfigs[i]));
            }
        }
        return transition;
    }

    public TransitionInformation CreateTransitionInformation(TransitionInforConfig config)
    {
        TransitionInformation information = new TransitionInformation();
        if (config != null)
        {
            information.boolNum = config.boolNum;
            information.doubleNum = config.doubleNum;
            information.floatNum = config.floatNum;
            information.intNum = config.intNum;
        }

        return information;
    }

    public Premise CreatePremise(PremiseConfig config)
    {
        BasePremise premise = PremiseRegisters.Instance.GetPremise(config.baseName);
        Premise premise1 = new Premise();
        premise1.premise = premise;
        premise1.premiseInformation = CreatePremiseInformation(config.inforConfig);

        return premise1;
    }

    public PremiseInformation CreatePremiseInformation(PremiseInforConfig config)
    {
        PremiseInformation information = new PremiseInformation();
        if (config != null)
        {
            information.boolNum = config.boolNum;
            information.doubleNum = config.doubleNum;
            information.floatNum = config.floatNum;
            information.intNum = config.intNum;
        }

        return information;
    }

    private bool IsBasic(string baseName)
    {
        for (int i = 0; i < basicStates.Length; i++)
        {
            if (baseName == basicStates[i])
            {
                return true;
            }
        }
        return false;
    }
}
