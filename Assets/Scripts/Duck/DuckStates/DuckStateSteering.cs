using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckStateSteering<T> : State<T>
{
    ISteering _steering;
    Duck _duck;
    ObstacleAvoidanceV2 _obs;

    public DuckStateSteering(Duck duck, ISteering steering, ObstacleAvoidanceV2 obs)
    {
        _steering = steering;
        _duck = duck;
        _obs = obs;
    }
    public override void Execute()
    {
        var dir = _obs.GetDir(_steering.GetDir(), false);
        _duck.Move(dir);
        _duck.LookDir(dir);
    }
}
