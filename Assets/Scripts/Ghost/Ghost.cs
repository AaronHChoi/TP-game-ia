using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Player
{
    [SerializeField] int life;
    public float attackCooldown;
    public Action onAttack = delegate { };
    Coroutine _cooldown;
    public void Attack()
    {
        _cooldown = StartCoroutine(Cooldown());
        onAttack();
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        _cooldown = null;
    }
    public bool IsCooldown => _cooldown != null;
    public int Life => life;
}
