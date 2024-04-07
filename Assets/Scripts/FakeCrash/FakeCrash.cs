using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCrash : Player
{
    [SerializeField]
    int _life;
    public float attackCooldown;
    Coroutine _cooldown;
    public Action onSpin = delegate { };
    public void Dead()
    {
        Destroy(gameObject);
    }
    public void Spin()
    {
        //Attack
        _cooldown = StartCoroutine(Cooldown());
        onSpin();
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        _cooldown = null;
    }
    public bool IsCooldown => _cooldown != null;
    public int Life => _life;
}
