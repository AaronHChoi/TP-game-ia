using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Player
{
    [SerializeField] int life;
    [SerializeField] float fuerzaEmpuje;
    public float attackCooldown;
    public Action onAttack = delegate { };
    Coroutine _cooldown;
    Rigidbody _rb;
    Animator _anim;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }
    public void Attack()
    {
        _cooldown = StartCoroutine(Cooldown());
        _anim.SetTrigger("Attack");
    }
    public override void Move(Vector3 dir)
    {
        dir *= speed;
        dir.y = _rb.velocity.y;
        _rb.velocity = dir;
    }
    public override void LookDir(Vector3 dir)
    {
        if (dir.x == 0 && dir.z == 0) return;
        transform.forward = dir;
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        _cooldown = null;
    }
    public bool IsCooldown => _cooldown != null;
    public int Life => life;
}
