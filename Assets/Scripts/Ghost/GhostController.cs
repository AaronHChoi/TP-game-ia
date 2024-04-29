using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public List<Transform> waypoints;
    public Transform target;
    public float attackRange;
    public float angle;
    public float radius;
    public LayerMask maskObs;
    Ghost _model;
    FSM<StatesEnum> _fsm;
    LineOfSight _los;
    IAlert _alert;
    ITreeNode _root;
    ObstacleAvoidance _obstacleAvoidance;

    ISteering _seek;
    ISteering _patrol;
    private void Awake()
    {
        _los = GetComponent<LineOfSight>();
        _alert = GetComponent<IAlert>();
        _model = GetComponent<Ghost>();
    }
    private void Start()
    {
        InitializeSteergin();
        InitializeFSM();
        InitializedTree();
    }
    void InitializeSteergin()
    {
        var seek = new Seek(_model.transform, target);
        var patrol = new Patrol(_model.transform, waypoints);
        _seek = seek;
        _patrol = patrol;
        _obstacleAvoidance = new ObstacleAvoidance(_model.transform, angle, radius, maskObs);
    }
    void InitializeFSM()
    {
        _fsm = new FSM<StatesEnum>();

        var idle = new GhostStateIdle<StatesEnum>();
        var attack = new GhostStateAttack<StatesEnum>(_model);
        var patrol = new GhostStateSteering<StatesEnum>(_model, _patrol, _obstacleAvoidance);
        var seek = new GhostStateSteering<StatesEnum>(_model, _seek, _obstacleAvoidance);

        idle.AddTransition(StatesEnum.Attack, attack);
        idle.AddTransition(StatesEnum.Idle, patrol);
        idle.AddTransition(StatesEnum.Seek, seek);

        attack.AddTransition(StatesEnum.Idle, idle);
        attack.AddTransition(StatesEnum.Patrol, patrol);
        attack.AddTransition(StatesEnum.Attack, attack);

        patrol.AddTransition(StatesEnum.Attack, attack);
        patrol.AddTransition(StatesEnum.Idle, idle);
        patrol.AddTransition(StatesEnum.Seek, seek);

        seek.AddTransition(StatesEnum.Attack, attack);
        seek.AddTransition(StatesEnum.Idle, idle);
        seek.AddTransition(StatesEnum.Patrol, patrol);

        _fsm.SetInit(patrol);
    }
    void InitializedTree()
    {
        var idle = new ActionNode(() => _fsm.Transition(StatesEnum.Idle));
        var attack = new ActionNode(() => _fsm.Transition(StatesEnum.Attack));
        var seek = new ActionNode(() => _fsm.Transition(StatesEnum.Seek));

        var dic = new Dictionary<ITreeNode, float>();
        //dic[dead] = 5;


        var qIsCooldown = new QuestionNode(() => _model.IsCooldown, idle, attack);
        var qAttackRange = new QuestionNode(QuestionAttackRange, qIsCooldown, seek);
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
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, angle / 2, 0) * transform.forward * radius);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -angle / 2, 0) * transform.forward * radius);
    }
}
