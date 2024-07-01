using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepStateSteering<T> : State<T>
{
    ISteering _steering;
    SheepModel _ghost;
    ObstacleAvoidance _obs;
    public SheepStateSteering(SheepModel ghost, ISteering steering, ObstacleAvoidance obs)
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
