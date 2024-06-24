using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckController : MonoBehaviour
{
    public float timePrediction;
    public float angle;
    public float radius;
    public LayerMask maskObs;
    DuckStateFollowPoints<StatesEnum> _stateFollowPoints;
    FSM<StatesEnum> _fsm;
    ISteering _steering;
    Duck _duck;
    ObstacleAvoidanceV2 _obstacleAvoidance;
    private void Awake()
    {
        _duck = GetComponent<Duck>();
        InitializeSteerings();
        InitializeFSM();
        InitializedTree();
    }

    void InitializeSteerings()
    {
        _steering = GetComponent<FlockingManager>();
        _obstacleAvoidance = new ObstacleAvoidanceV2(_duck.transform, angle, radius, maskObs);
    }

    void InitializeFSM()
    {
        _fsm = new FSM<StatesEnum>();

        var idle = new DuckStateIdle<StatesEnum>();
        var steering = new DuckStateSteering<StatesEnum>(_duck, _steering, _obstacleAvoidance);
        _stateFollowPoints = new DuckStateFollowPoints<StatesEnum>(_duck);

        idle.AddTransition(StatesEnum.Walk, steering);
        idle.AddTransition(StatesEnum.Waypoints, _stateFollowPoints);

        steering.AddTransition(StatesEnum.Idle, idle);
        steering.AddTransition(StatesEnum.Waypoints, _stateFollowPoints);

        _stateFollowPoints.AddTransition(StatesEnum.Idle, idle);
        _stateFollowPoints.AddTransition(StatesEnum.Walk, steering);

        _fsm.SetInit(steering);
    }
    void InitializedTree()
    {
        var idle = new ActionNode(() => _fsm.Transition(StatesEnum.Idle));
        var walk = new ActionNode(() => _fsm.Transition(StatesEnum.Walk));
        var follow = new ActionNode(() => _fsm.Transition(StatesEnum.Waypoints));

        var qFollowPoints = new QuestionNode(() => _stateFollowPoints.IsFinishPath, idle, follow);
        
    }
    void Update()
    {
        _fsm.OnUpdate();
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
