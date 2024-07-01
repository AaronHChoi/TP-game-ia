using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SheepController : EnemyController
{
    SheepModel _model;
    SheepStateFollowPoints<StatesEnum> _stateFollowPoints;
    [SerializeField] Animator _anim;
    ISteering _evade;
    ISteering _seek;
    Coroutine _chaseCoroutine;
    Coroutine _evadeCoroutine;
    protected override void Awake()
    {
        base.Awake();
        _model = GetComponent<SheepModel>();
        _stateFollowPoints = new SheepStateFollowPoints<StatesEnum>(_model, _anim);
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void InitializeSteering()
    {
        var seek = new Seek(_model.transform, target);
        var evade = new Evade(_model.transform, rbTarget, timePrediction);
        _seek = seek;
        _evade = evade;
        _obstacleAvoidance = new ObstacleAvoidance(_model.transform, angle, radius, maskObs);
    }
    protected override void InitializeFSM()
    {
        _fsm = new FSM<StatesEnum>();

        var idle = new SheepStateIdle<StatesEnum>();
        var seek = new SheepStateSteering<StatesEnum>(_model, _seek, _obstacleAvoidance);
        var evade = new SheepStateSteering<StatesEnum>(_model, _evade, _obstacleAvoidance);

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
    protected override void InitializeTree()
    {
        var idle = new ActionNode(() => _fsm.Transition(StatesEnum.Idle));
        var seek = new ActionNode(() => _fsm.Transition(StatesEnum.Seek));
        var follow = new ActionNode(() => _fsm.Transition(StatesEnum.Waypoints));
        var evade = new ActionNode(() => _fsm.Transition(StatesEnum.Evade));

        //var dic = new Dictionary<ITreeNode, float>();

        var qFollowPoints = new QuestionNode(() => _stateFollowPoints.IsFinishPath, idle, follow);

        var qKeepChaising = new QuestionNode(QuestionChaseTime, seek, qFollowPoints);
        var qIsCooldown = new QuestionNode(() => _model.IsCooldown, idle, seek);
        var qIsCooldownOutOfRange = new QuestionNode(() => _model.IsCooldown, idle, seek);
        var qAttackRange = new QuestionNode(QuestionAttackRange, qIsCooldown, qIsCooldownOutOfRange);
        var qSizeCheck = new QuestionNode(() => size.playerValue >= 3, evade, qAttackRange);
        var qLos = new QuestionNode(QuestionLoS, qSizeCheck, qFollowPoints);
        var qHasLife = new QuestionNode(() => _model.Life > 0, qLos, idle);

        _root = qHasLife;
    }
    protected override IEnumerator EvadeTime()
    {
        yield return new WaitForSeconds(evadeTime);
        seen = false;
    }
    protected override bool QuestionChaseTime()
    {
        StartCoroutine(ChaseTime());
        return true;
    }
    protected override bool QuestionAttackRange()
    {
        return _los.CheckRange(target.transform, attackRange);
    }
    protected override bool QuestionLoS()
    {
        var currLoS = _los.CheckRange(target.transform) && _los.CheckAngle(target.transform) && _los.CheckView(target.transform);
        if (!currLoS && seen)
        {
            if (_chaseCoroutine == null)
            {
                _chaseCoroutine = StartCoroutine(ChaseTime());
            }
            if (_evadeCoroutine == null)
            {
                _evadeCoroutine = StartCoroutine(EvadeTime());
            }
            return true;
        }
        if (_chaseCoroutine != null)
        {
            StopCoroutine(ChaseTime());
            _chaseCoroutine = null;
        }
        if (_evadeCoroutine != null)
        {
            StopCoroutine(EvadeTime());
            _evadeCoroutine = null;
        }
        seen = currLoS;
        return seen;
    }
    protected override IEnumerator ChaseTime()
    {
        yield return new WaitForSeconds(chaseTime);
        seen = false;
    }
    protected override void Update()
    {
        base.Update();
    }
    public IPoints GetStateWaypoints => _stateFollowPoints;
    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
    }
}