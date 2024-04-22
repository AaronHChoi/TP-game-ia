using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<<< HEAD:Assets/Scripts/Ghost/GhostStates/GhostStateAttack.cs
public class GhostStateAttack<T> : State<T>
{
    Ghost _model;
    public GhostStateAttack(Ghost model)
========
public class EnemyAttackState<T> : State<T>
{
    Enemy _model;
    public EnemyAttackState(Enemy model)
>>>>>>>> Test:Assets/Scripts/Ghost/GhostStates/EnemyAttackState.cs
    {
        _model = model;
    }
    public override void Execute()
    {
        base.Execute();
        _model.Attack();
    }
}
