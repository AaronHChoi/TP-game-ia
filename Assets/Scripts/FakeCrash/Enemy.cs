using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Player
{
    [SerializeField]
    int _life;
    public float attackCooldown;
    Coroutine _cooldown;
    public Action onSpin = delegate { };
    public float fuerzaEmpuje = 1000f;
    public GameObject personaje;

    public void Dead()
    {
        Destroy(gameObject);
    }
    public void Spin()
    {
        Attack();
        _cooldown = StartCoroutine(Cooldown());
        onSpin();
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        _cooldown = null;
    }

    void Attack()
    {
        Debug.Log("atack");

        //Vector3 direccionOpuesta = personaje.transform.position - transform.position;
        //direccionOpuesta.Normalize(); // Normalizar para obtener una dirección unitaria

        //// Aplicar fuerza al personaje en la dirección opuesta
        //Rigidbody personajeRigidbody = personaje.GetComponent<Rigidbody>();
        //personajeRigidbody.AddForce(direccionOpuesta * fuerzaEmpuje);

        personaje.transform.position += transform.forward * Time.deltaTime * fuerzaEmpuje;
    }
    public bool IsCooldown => _cooldown != null;
    public int Life => _life;
}
