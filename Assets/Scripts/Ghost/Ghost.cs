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
    //[SerializeField] Ghost _model;
    private Rigidbody _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void Attack()
    {
        _cooldown = StartCoroutine(Cooldown());
        onAttack();
        //_model.transform.position += transform.forward * Time.deltaTime * fuerzaEmpuje;

        //version 2

        //Vector3 direccionOpuesta = _model.transform.position - transform.position;
        //direccionOpuesta.Normalize();

        //Rigidbody personajeRigidbody = _model.GetComponent<Rigidbody>();
        //personajeRigidbody.AddForce(direccionOpuesta * fuerzaEmpuje);
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
