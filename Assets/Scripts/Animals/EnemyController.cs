using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    public Player size;
    public Transform target;
    public Rigidbody rbTarget;
    public float timePrediction;
    public float attackRange;
    public float angle;
    public float radius;
    public LayerMask maskObs;
    public float chaseTime;
    public float evadeTime;
    public bool seen;

    protected FSM<StatesEnum> _fsm;
    protected LineOfSight _los;
    protected ITreeNode _root;
    protected ObstacleAvoidance _obstacleAvoidance;

    protected abstract void InitializeSteering();
    protected abstract void InitializeFSM();
    protected abstract void InitializeTree();

    protected abstract IEnumerator EvadeTime();
    protected abstract bool QuestionChaseTime();
    protected abstract bool QuestionAttackRange();
    protected abstract bool QuestionLoS();
    protected abstract IEnumerator ChaseTime();
    protected virtual void Awake()
    {
        _los = GetComponent<LineOfSight>();
    }
    protected virtual void Start()
    {
        InitializeSteering();
        InitializeFSM();
        InitializeTree();
    }
    protected virtual void Update()
    {
        _fsm.OnUpdate();
        _root.Execute();
    }
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, angle / 2, 0) * transform.forward * radius);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -angle / 2, 0) * transform.forward * radius);
    }
}