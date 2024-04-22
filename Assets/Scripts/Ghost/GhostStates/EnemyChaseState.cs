using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<<< HEAD:Assets/Scripts/Ghost/GhostStates/GhostStateChase.cs
public class GhostStateChase<T> : State<T>
{
    Ghost _model;
    Transform _target;
    public GhostStateChase(Ghost model, Transform target)
========
public class EnemyChaseState<T> : State<T>
{
    Enemy _model;
    Transform _target;

    public EnemyChaseState(Enemy model, Transform target)
>>>>>>>> Test:Assets/Scripts/Ghost/GhostStates/EnemyChaseState.cs
    {
        _model = model;
        _target = target;
    }
    public override void Execute()
    {
        base.Execute();

        Vector3 dir = _target.position - _model.transform.position;

        _model.Move(dir.normalized);
        _model.LookDir(dir.normalized);
    }
}
