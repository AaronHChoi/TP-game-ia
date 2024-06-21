using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : ISteering
{
    Transform _entity;
    List<Transform> _waypoints;
    int _currentWaypoint = 0;

    public Patrol(Transform entity, List<Transform> waypoints)
    {
        _entity = entity;
        _waypoints = waypoints;
        MyRandoms.Shuffle(_waypoints);
    }
    public Vector3 GetDir()
    {
        if(_waypoints.Count == 0)
            return Vector3.zero;

        Vector3 targetDir = _waypoints[_currentWaypoint].position - _entity.position;

        if(targetDir.magnitude < 1.0f)
        {
            _currentWaypoint = (_currentWaypoint + 1) % _waypoints.Count;
            if(_currentWaypoint == 0)
            {
                _waypoints.Reverse();
                _currentWaypoint = 0;
            }
            targetDir = _waypoints[_currentWaypoint].position - _entity.position;
        }
        return targetDir.normalized;
    }
}
