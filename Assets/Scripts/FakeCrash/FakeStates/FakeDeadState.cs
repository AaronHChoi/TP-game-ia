using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeDeadState<T> : State<T>
{
    FakeCrash _model;

    public FakeDeadState(FakeCrash model)
    {
        _model = model;
    }

    public override void Enter()
    {
        base.Enter();
        _model.Dead();
    }
}
