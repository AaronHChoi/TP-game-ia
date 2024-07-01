using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckController : EnemyController
{
    DuckStateFollowPoints<StatesEnum> _stateFollowPoints;
    ISteering _steering;
    Duck _duck;
    ObstacleAvoidanceV2 _obstacleAvoidancev2;
    protected override void Awake()
    {
        base.Awake();
        _duck = GetComponent<Duck>();
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void InitializeSteering()
    {
        _steering = GetComponent<FlockingManager>();
        _obstacleAvoidancev2 = new ObstacleAvoidanceV2(_duck.transform, angle, radius, maskObs);
    }
    protected override void InitializeFSM()
    {
        _fsm = new FSM<StatesEnum>();

        var idle = new DuckStateIdle<StatesEnum>();
        var steering = new DuckStateSteering<StatesEnum>(_duck, _steering, _obstacleAvoidancev2);
        _stateFollowPoints = new DuckStateFollowPoints<StatesEnum>(_duck);

        idle.AddTransition(StatesEnum.Walk, steering);
        idle.AddTransition(StatesEnum.Waypoints, _stateFollowPoints);

        steering.AddTransition(StatesEnum.Idle, idle);
        steering.AddTransition(StatesEnum.Waypoints, _stateFollowPoints);

        _stateFollowPoints.AddTransition(StatesEnum.Idle, idle);
        _stateFollowPoints.AddTransition(StatesEnum.Walk, steering);

        _fsm.SetInit(steering);
    }
    protected override void InitializeTree()
    {
        var idle = new ActionNode(() => _fsm.Transition(StatesEnum.Idle));
        var walk = new ActionNode(() => _fsm.Transition(StatesEnum.Walk));
        var follow = new ActionNode(() => _fsm.Transition(StatesEnum.Waypoints));

        var qFollowPoints = new QuestionNode(() => _stateFollowPoints.IsFinishPath, idle, follow);
        
    }
    protected override void Update()
    {
        _fsm.OnUpdate();
    }
    public IPoints GetStateWaypoints => _stateFollowPoints;
    protected override void OnDrawGizmosSelected()
    {
       base.OnDrawGizmosSelected();
    }
    protected override IEnumerator EvadeTime()
    {
        yield break;
    }
    protected override bool QuestionChaseTime()
    {
        return false;
    }
    protected override bool QuestionAttackRange()
    {
        return false;
    }
    protected override bool QuestionLoS()
    {
        return false;
    }
    protected override IEnumerator ChaseTime()
    {
        yield break;
    }
}