using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : PlayerView
{
    Enemy _model;
    protected override void Awake()
    {
        base.Awake();
        _model = GetComponent<Enemy>();
        _model.onSpin += OnSpin;
    }
    void OnSpin()
    {
        anim.SetTrigger("Spin");
    }
}
