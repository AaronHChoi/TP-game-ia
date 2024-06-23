using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour, IBoid
{
    public float speed;
    public float speedRot = 5;
    public Vector3 Position => transform.position;
    public Vector3 Front => transform.forward;
    Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void Move(Vector3 dir)
    {
        dir *= speed;
        dir.y = _rb.velocity.y;
        _rb.velocity = dir;
    }
    public void LookDir(Vector3 dir)
    {
        if (dir.x == 0 && dir.z == 0) return;
        transform.forward = Vector3.Lerp(transform.forward, dir, Time.deltaTime * speedRot);
    }
}