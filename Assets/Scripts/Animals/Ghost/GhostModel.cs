using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostModel : MonoBehaviour
{
    public float speed = 2;
    public float speedRot = 10;
    [SerializeField] int life;
    [SerializeField] float fuerzaEmpuje;
    public float attackCooldown;
    public Action onAttack = delegate { };
    Coroutine _cooldown;
    public void Attack()
    {
        _cooldown = StartCoroutine(Cooldown());
        onAttack();
    }
    public void Move(Vector3 dir)
    {
        dir.y = 0;
        transform.position += Time.deltaTime * dir * speed; ;
    }
    public void LookDir(Vector3 dir)
    {
        if (Vector3.Angle(transform.forward, dir) > (Mathf.PI * Mathf.Rad2Deg) / 2)
        {
            transform.forward = dir;
        }
        else
        {
            transform.forward = Vector3.Lerp(transform.forward, dir, speedRot * Time.deltaTime);
        }
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        _cooldown = null;
    }
    public bool IsCooldown => _cooldown != null;
    public int Life => life;
    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }
}
