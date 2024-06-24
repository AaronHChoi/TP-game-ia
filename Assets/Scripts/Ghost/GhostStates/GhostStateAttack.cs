using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStateAttack<T> : State<T>
{
    GhostModel _model;
    public GhostStateAttack(GhostModel model)
    {
        _model = model;
    }
    public override void Execute()
    {
        base.Execute();
        _model.Attack();
    }
}
