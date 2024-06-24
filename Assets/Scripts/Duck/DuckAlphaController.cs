using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckAlphaController : MonoBehaviour
{
    DuckAlphaModel _model;
    Animator _anim;
    FSM<StatesEnum> _fsm;
    AlphaDuckStateFollowPoints<StatesEnum> _stateFollowPoints;
    ITreeNode _root;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _model = GetComponent<DuckAlphaModel>();
    }
    private void Start()
    {
        InitializeFSM();
        InitializeTree();
    }

    void InitializeFSM()
    {
        _fsm = new FSM<StatesEnum>();
        var idle = new DuckStateIdle<StatesEnum>();
        _stateFollowPoints = new AlphaDuckStateFollowPoints<StatesEnum>(_model);

        idle.AddTransition(StatesEnum.Waypoints, _stateFollowPoints);
        _stateFollowPoints.AddTransition(StatesEnum.Idle, idle);
        _fsm.SetInit(idle);
    }
    void InitializeTree()
    {
        var idle = new ActionNode(() => _fsm.Transition(StatesEnum.Idle));
        var follow = new ActionNode(() => _fsm.Transition(StatesEnum.Waypoints));

        var qFollowPoints = new QuestionNode(() => _stateFollowPoints.IsFinishPath, idle, follow);
        _root = qFollowPoints;
    }
    private void Update()
    {
        _fsm.OnUpdate();
        _root.Execute();
    }
    public IPoints GetStateWaypoints => _stateFollowPoints;
}
