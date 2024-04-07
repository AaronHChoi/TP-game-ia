using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeAttackState<T> : State<T>
{
    FakeCrash _model;
    public FakeAttackState(FakeCrash model)
    {
        _model = model;
    }
    public override void Execute()
    {
        base.Execute();
        _model.Spin();
    }
}
