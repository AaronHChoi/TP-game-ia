using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCrashView : PlayerView
{
    FakeCrash _model;
    protected override void Awake()
    {
        base.Awake();
        _model = GetComponent<FakeCrash>();
        _model.onSpin += OnSpin;
    }
    void OnSpin()
    {
        anim.SetTrigger("Spin");
    }
}
