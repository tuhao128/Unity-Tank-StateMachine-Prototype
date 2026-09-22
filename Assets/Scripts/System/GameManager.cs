using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

//以这个系统为统筹者，负责完全管理其他系统的运行操作
//这个Manager负责放在物体上，这个Manager内部只能有其他Manager
public class GameManager : MonoBehaviour
{
    public int MaxActions;
    public CondSenderRegisters condSenderRegisters;
    public ControInstanceRegisters instanceRegisters;
    public BroadCasterRegisters casterRegisters;
    public StateRegisters stateRegisters;
    public TransitionRegisters transitionRegisters;
    public PremiseRegisters premiseRegisters;
    private void Awake()
    {
        ControlsManager.InitControlsManager();

        //InstanceManager.InitInstances();
        instanceRegisters.InitInstance();
        instanceRegisters.Register();

        ActionManager.InitInstance(MaxActions);


        //BroadcasterManager.InitInstance();
        //BroadcasterManager.Instance.InitRegisters();
        casterRegisters.InitInstance();
        casterRegisters.Register();

        StateClipFactory.InitInstance();

        //StatesManager.InitInstance();
        //StatesManager.Instance.InitRegisters();
        stateRegisters.InitInstance();
        stateRegisters.Register();

        //TransitionsManager.InitInstance();
        //TransitionsManager.Instance.InitRegisters();
        transitionRegisters.InitInstance();
        transitionRegisters.Register();

        //PremisesManager.InitInstance();
        //PremisesManager.Instance.InitRegisters();
        premiseRegisters.InitInstance();
        premiseRegisters.Register();

        //CondSendersManager.InitInstance();
        //CondSendersManager.Instance.InitRegisters();
        condSenderRegisters.InitInstance();
        condSenderRegisters.Register();
    }

    //------------这里缓存区读取操作留白，可能要另外处理
    private void Update()
    {
        //每个动作帧开始先把数据从缓存区取出来
        ActionManager.Instance.UpdateDatas();
        //然后DataManager会随时通过action事件发布行为包给ActionManager
        ActionManager.Instance.Update();
        //这里ActionManager直接读取DataManager统一发出的信息，然后执行
    }

    private void FixedUpdate()
    {
        ActionManager.Instance.FixedUpdateDatas();
        ActionManager.Instance.FixedUpdate(); 
        //这里ActionManager直接读取DataManager统一发出的信息，然后执行
    }

    //private void OnDisable()
    //{
        //InstanceManager.OnDisable();
    //}

    //private void OnDestroy()
    //{
        //InstanceManager.Clear();
    //}
}
