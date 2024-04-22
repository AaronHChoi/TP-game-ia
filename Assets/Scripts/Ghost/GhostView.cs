using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostView : MonoBehaviour
{
    public GameObject alertUI;
    Ghost _model;
    IAlert _alert;
    private void Awake()
    {
        _alert = GetComponent<IAlert>();
    }
    private void Start()
    {
        _model = GetComponent<Ghost>();
        _model.onAttack += OnAttack;
    }
    private void Update()
    {
        alertUI.SetActive(_alert.Alert);
    }
    void OnAttack()
    {
        Debug.Log("Attack");
    }
}
