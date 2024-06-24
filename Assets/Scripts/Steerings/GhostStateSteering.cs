using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStateSteering<T> : State<T>
{
    ISteering _steering;
    GhostModel _ghost;
    ObstacleAvoidance _obs;
    public GhostStateSteering(GhostModel ghost, ISteering steering, ObstacleAvoidance obs)
    {
        _steering = steering;
        _ghost = ghost;
        _obs = obs;
    }
    public override void Execute()
    {
        var dir = _obs.GetDir(_steering.GetDir());
        _ghost.Move(dir);
        _ghost.LookDir(dir);
        //Debug.Log("Seek");
    }
}
