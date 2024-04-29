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
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        _cooldown = null;
    }
    public bool IsCooldown => _cooldown != null;
    public int Life => life;
}
