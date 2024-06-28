using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepController : MonoBehaviour
{
    public SizeManager size;
    public Transform target;
    public Rigidbody rbTarget;
    public float timePrediction;
    public float attackRange;
    public float angle;
    public float radius;
    public LayerMask maskObs;
    [SerializeField] float chaseTime;
    bool seen;
    SheepModel _model;
    FSM<StatesEnum> _fsm;
    LineOfSight _los;
    ITreeNode _root;
    ObstacleAvoidance _obstacleAvoidance;
    SheepStateFollowPoints<StatesEnum> _stateFollowPoints;
    [SerializeField] Animator _anim;
    ISteering _evade;
    ISteering _seek;
    //ISteering _patrol;
    Coroutine _chaseCoroutine;
    private void Awake()
    {
        _los = GetComponent<LineOfSight>();
        _model = GetComponent<SheepModel>();
        _stateFollowPoints = new SheepStateFollowPoints<StatesEnum>(_model, _anim);
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
        var evade = new Evade(_model.transform, rbTarget, timePrediction);
        _seek = seek;
        _evade = evade;
        _obstacleAvoidance = new ObstacleAvoidance(_model.transform, angle, radius, maskObs);
    }
    void InitializeFSM()
    {
        _fsm = new FSM<StatesEnum>();

        var idle = new SheepStateIdle<StatesEnum>();
        var seek = new SheepStateSteering<StatesEnum>(_model, _seek, _obstacleAvoidance);
        var evade = new SheepStateSteering<StatesEnum>(_model, _seek, _obstacleAvoidance);
        

        idle.AddTransition(StatesEnum.Seek, seek);
        idle.AddTransition(StatesEnum.Waypoints, _stateFollowPoints);
        idle.AddTransition(StatesEnum.Evade, evade);

        seek.AddTransition(StatesEnum.Idle, idle);
        seek.AddTransition(StatesEnum.Waypoints, _stateFollowPoints);
        seek.AddTransition(StatesEnum.Evade, evade);

        _stateFollowPoints.AddTransition(StatesEnum.Idle, idle);
        _stateFollowPoints.AddTransition(StatesEnum.Seek, seek);
        _stateFollowPoints.AddTransition(StatesEnum.Evade, evade);

        evade.AddTransition(StatesEnum.Idle, idle);
        evade.AddTransition(StatesEnum.Seek, seek);
        evade.AddTransition(StatesEnum.Waypoints, _stateFollowPoints);

        _fsm.SetInit(idle);
    }
    void InitializedTree()
    {
        var idle = new ActionNode(() => _fsm.Transition(StatesEnum.Idle));
        var attack = new ActionNode(() => _fsm.Transition(StatesEnum.Attack));
        var seek = new ActionNode(() => _fsm.Transition(StatesEnum.Seek));
        var follow = new ActionNode(() => _fsm.Transition(StatesEnum.Waypoints));
        //var evade = new ActionNode(() => StartEvade());

        //var dic = new Dictionary<ITreeNode, float>();

        var qFollowPoints = new QuestionNode(() => _stateFollowPoints.IsFinishPath, idle, follow);

        var qKeepChaising = new QuestionNode(QuestionChaseTime, seek, qFollowPoints);
        var qIsCooldown = new QuestionNode(() => _model.IsCooldown, idle, seek);
        var qIsCooldownOutOfRange = new QuestionNode(() => _model.IsCooldown, idle, seek);
        var qAttackRange = new QuestionNode(QuestionAttackRange, qIsCooldown, qIsCooldownOutOfRange);
        //var qHasSize = new QuestionNode(() => size.playerValue >= 2, evade, qAttackRange);
        var qLos = new QuestionNode(QuestionLoS, qAttackRange/*qHasSize*/, qFollowPoints);
        var qHasLife = new QuestionNode(() => _model.Life > 0, qLos, idle);

        _root = qHasLife;
    }
    bool QuestionChaseTime()
    {
        StartCoroutine(ChaseTime());
        return true;
    }
    bool QuestionAttackRange()
    {
        return _los.CheckRange(target.transform, attackRange);
    }
    bool QuestionLoS()
    {
        var currLoS = _los.CheckRange(target.transform) && _los.CheckAngle(target.transform) && _los.CheckView(target.transform);
        if (!currLoS && seen)
        {
            if (_chaseCoroutine == null)
            {
                _chaseCoroutine = StartCoroutine(ChaseTime());
            }
            return true;
        }
        if (_chaseCoroutine != null)
        {
            StopCoroutine(ChaseTime());
            _chaseCoroutine = null;
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
        Debug.Log("Player size value: " + size.playerValue);
        _root.Execute();
    }
    public IPoints GetStateWaypoints => _stateFollowPoints;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, angle / 2, 0) * transform.forward * radius);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -angle / 2, 0) * transform.forward * radius);
    }
}
