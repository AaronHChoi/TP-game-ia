using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStateAttack<T> : State<T>
{
    Ghost _model;
    public GhostStateAttack(Ghost model)
    {
        _model = model;
    }
    public override void Execute()
    {
        base.Execute();
        _model.Attack();
    }
}
