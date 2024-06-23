using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostView : MonoBehaviour
{
    Ghost _model;
    Rigidbody _rb;
    [SerializeField]GameManager _gameManager;
    //public Animator anim;

    private void Start()
    {
        _model = GetComponent<Ghost>();
        _rb = GetComponent<Rigidbody>();
        _model.onAttack += OnAttack;
    }
    private void Update()
    {
        //anim.SetFloat("Vel", _rb.velocity.magnitude);
    }
    void OnAttack()
    {
        Debug.Log("Attack");
        _gameManager.EndGame("Lose");
    }
}
