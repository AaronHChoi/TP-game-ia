using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    ILineOfSight _los;
    IAlert _alert;
    ITreeNode _root;
    FSM<CameraStateEnum> _fsm;
    private void Awake()
    {
        _los = GetComponent<ILineOfSight>();
        _alert = GetComponent<IAlert>();
        InitializeFSM();
        InitializeTree();
    }
    void InitializeFSM()
    {
        _fsm = new FSM<CameraStateEnum>();

        var normal = new State<CameraStateEnum>();
        var alert = GetComponent<CameraStateAlert>();

        normal.AddTransition(CameraStateEnum.Alert, alert);
        alert.AddTransition(CameraStateEnum.Normal, normal);

        _fsm.SetInit(normal);
    }
    void InitializeTree()
    {
        //Action
        var alert = new ActionNode(() => _fsm.Transition(CameraStateEnum.Alert));
        var normal = new ActionNode(ActionNormal);
        //Questions
        var qLoS = new QuestionNode(QuestionLoS, alert, normal);

        _root = qLoS;
    }
    private void Update()
    {
        _fsm.OnUpdate();
        _root.Execute();
    }
    bool QuestionLoS()
    {
        return _los.CheckRange(target)
            && _los.CheckAngle(target)
            && _los.CheckView(target);
    }
    void ActionNormal()
    {
        _fsm.Transition(CameraStateEnum.Normal);
    }
}
