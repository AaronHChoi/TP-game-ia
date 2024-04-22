using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public class Player : MonoBehaviour, IPlayerModel
{
    public float speed;
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
        //transform.Translate(dir, Space.Self);
    }
    public void LookDir(Vector3 dir)
    {
        if (dir.x == 0 && dir.z == 0) return;
        transform.forward = dir;
    }
}
