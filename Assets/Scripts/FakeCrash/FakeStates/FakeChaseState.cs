using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeChaseState<T> : State<T>
{
    FakeCrash _model;
    Transform _target;

    public FakeChaseState(FakeCrash model, Transform target)
    {
        _model = model;
        _target = target;
    }
    public override void Execute()
    {
        base.Execute();

        //A: FakeCrash
        //B: Target

        //(b-a).n
        Vector3 dir = _target.position - _model.transform.position;
        _model.Move(dir.normalized);
        _model.LookDir(dir.normalized);
    }
}
