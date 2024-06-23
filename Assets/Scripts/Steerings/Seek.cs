using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : ISteering
{
    Transform _entity;
    Transform _target;
    public Seek(Transform entity, Transform target)
    {
        _entity = entity;
        _target = target;
    }
    public Seek(Transform target)
    {
        _target = target;
    }
    public Vector3 GetDir()
    {
        Vector3 dir = (_target.position - _entity.position).normalized;
        return dir;
    }
    public Transform Entity
    {
        set
        {
            _entity = value;
        }
    }
}