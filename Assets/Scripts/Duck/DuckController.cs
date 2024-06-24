using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckController : MonoBehaviour
{
    public float timePrediction;
    public float angle;
    public float radius;
    public LayerMask maskObs;
    FSM<StatesEnum> _fsm;
    ISteering _steering;
    Duck _duck;
    ObstacleAvoidanceV2 _obstacleAvoidance;
    private void Awake()
    {
        _duck = GetComponent<Duck>();
        InitializeSteerings();
        InitializeFSM();
    }

    void InitializeSteerings()
    {
        //var seek = new Seek(_duck.transform, target.transform);
        //var flee = new Flee(_duck.transform, target.transform);
        //var pursuit = new Pursuit(_duck.transform, target, timePrediction);
        //var evade = new Evade(_duck.transform, target, timePrediction);
        _steering = GetComponent<FlockingManager>();
        _obstacleAvoidance = new ObstacleAvoidanceV2(_duck.transform, angle, radius, maskObs);
    }

    void InitializeFSM()
    {
        _fsm = new FSM<StatesEnum>();

        var idle = new DuckStateIdle<StatesEnum>();
        var steering = new DuckStateSteering<StatesEnum>(_duck, _steering, _obstacleAvoidance);

        idle.AddTransition(StatesEnum.Walk, steering);
        steering.AddTransition(StatesEnum.Idle, idle);

        _fsm.SetInit(steering);
    }
    void Update()
    {
        _fsm.OnUpdate();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, angle / 2, 0) * transform.forward * radius);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -angle / 2, 0) * transform.forward * radius);
    }
}
