using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CrashController : MonoBehaviour
{
    CrashModel _model;
    Animator _anim;
    FSM<StatesEnum> _fsm;
    CrashStateFollowPoints<StatesEnum> _stateFollowPoints;
    ITreeNode _root;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _model = GetComponent<CrashModel>();
    }
    private void Start()
    {
        InitializeFSM();
        InitializeTree();
    }

    void InitializeFSM()
    {
        _fsm = new FSM<StatesEnum>();
        //var idle = new CrashStateIdle<StatesEnum>(_anim);
        _stateFollowPoints = new CrashStateFollowPoints<StatesEnum>(_model, _anim);

        //idle.AddTransition(StatesEnum2.Waypoints, _stateFollowPoints);
        //_stateFollowPoints.AddTransition(StatesEnum2.Idle, idle);
        //_fsm.SetInit(idle);
    }
    void InitializeTree()
    {
        var idle = new ActionNode(() => _fsm.Transition(StatesEnum.Idle));
        //var follow = new ActionNode(() => _fsm.Transition(StatesEnum.Waypoints));

        //var qFollowPoints = new QuestionNode(() => _stateFollowPoints.IsFinishPath, idle, follow);
        //_root = qFollowPoints;
    }
    private void Update()
    {
        _fsm.OnUpdate();
        _root.Execute();
    }
    public IPoints GetStateWaypoints => _stateFollowPoints;
}
