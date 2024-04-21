using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Player
{
    public float attackCooldown;
    Coroutine _cooldown;
    public void Attack()
    {
        _cooldown = StartCoroutine(Cooldown());
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        Debug.Log("Attack");
    }
    public bool IsCooldown => _cooldown != null;
}
