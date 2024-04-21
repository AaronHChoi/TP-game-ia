using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public Transform target;
    public float attackRange;
    Ghost _model;
    FSM<StatesEnum> _fsm;
    LineOfSight _los;
    IAlert _alert;
    ITreeNode _root;
    private void Awake()
    {
        _los = GetComponent<LineOfSight>();
        _alert = GetComponent<IAlert>();
        _model = GetComponent<Ghost>();
    }
    private void Start()
    {
        InitializeFSM();
        InitializedTree();
    }
    void InitializeFSM()
    {
        var idle = new GhostStateIdle<StatesEnum>();
        var attack = new GhostStateAttack<StatesEnum>(_model);
        var walk = new GhostStateWalk<StatesEnum>();
        var chase = new GhostStateChase<StatesEnum>(_model, target);

        idle.AddTransition(StatesEnum.Attack, attack);
        idle.AddTransition(StatesEnum.Walk, walk);
        idle.AddTransition(StatesEnum.Chase, chase);

        attack.AddTransition(StatesEnum.Idle, idle);
        attack.AddTransition(StatesEnum.Walk, walk);
        attack.AddTransition(StatesEnum.Chase, chase);

        walk.AddTransition(StatesEnum.Attack, attack);
        walk.AddTransition(StatesEnum.Idle, idle);
        walk.AddTransition(StatesEnum.Chase, chase);

        chase.AddTransition(StatesEnum.Attack, attack);
        chase.AddTransition(StatesEnum.Walk, walk);
        chase.AddTransition(StatesEnum.Idle, idle);

        _fsm = new FSM<StatesEnum>(idle);
    }
    void InitializedTree()
    {
        var idle = new ActionNode(() => _fsm.Transition(StatesEnum.Idle));
        var attack = new ActionNode(() => _fsm.Transition(StatesEnum.Attack));
        //var walk = new ActionNode(() => _fsm.Transition(StatesEnum.Walk));
        var chase = new ActionNode(() => _fsm.Transition(StatesEnum.Chase));

        var qIsCooldown = new QuestionNode(() => _model.IsCooldown, idle, attack);
        var qAttackRange = new QuestionNode(QuestionAttackRange, qIsCooldown, chase);
        var qLos = new QuestionNode(QuestionLoS, qAttackRange, idle);

        _root = qLos;
    }
    bool QuestionAttackRange()
    {
        return _los.CheckRange(target, attackRange);
    }
    bool QuestionLoS()
    {
        return _los.CheckRange(target) && _los.CheckAngle(target) && _los.CheckView(target);
    }
    private void Update()
    {
        _fsm.OnUpdate();
        _root.Execute();

        if (_los.CheckRange(target) && _los.CheckAngle(target) && _los.CheckView(target)) 
        {
            _alert.Alert = true;
        }
        else
        {
            _alert.Alert = false;
        }
    }
}
