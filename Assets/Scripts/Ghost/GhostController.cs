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
    [SerializeField]float chaseTime;
    bool seen;
    Ghost _model;
    FSM<StatesEnum> _fsm;
    LineOfSight _los;
    ITreeNode _root;
    ObstacleAvoidance _obstacleAvoidance;
    [SerializeField] Animator _anim;
    ISteering _seek;
    ISteering _patrol;
    Coroutine _coroutine;
    private void Awake()
    {
        _los = GetComponent<LineOfSight>();
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
        var patrol = new ActionNode(() => _fsm.Transition(StatesEnum.Patrol));

        //var dic = new Dictionary<ITreeNode, float>();

        var qIsCooldown = new QuestionNode(() => _model.IsCooldown, idle, attack);
        var qIsCooldownOutOfRange = new QuestionNode(() => _model.IsCooldown, idle, seek);
        var qAttackRange = new QuestionNode(QuestionAttackRange, qIsCooldown, qIsCooldownOutOfRange);
        var qLos = new QuestionNode(QuestionLoS, qAttackRange, idle);
        var qHasLife = new QuestionNode(() => _model.Life > 0, qLos, idle);

        _root = qLos;
    }
    bool QuestionAttackRange()
    {
        return _los.CheckRange(target, attackRange);
    }
    bool QuestionLoS()
    {
        var currLoS = _los.CheckRange(target) && _los.CheckAngle(target) && _los.CheckView(target);
        if(currLoS == false && seen == true)
        {
            if(_coroutine == null)
            {
                _coroutine = StartCoroutine(ChaseTime());
            }
        }
        if(_coroutine != null)
        {
            StopCoroutine(ChaseTime());
            _coroutine = null;
        }
        seen = currLoS;
        return seen;
    }
    IEnumerator ChaseTime()
    {
        yield return new WaitForSeconds(chaseTime);
        seen = false;   
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