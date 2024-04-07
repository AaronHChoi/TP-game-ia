using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraStateAlert : StateMono<CameraStateEnum>
{
    IAlert _alert;
    private void Awake()
    {
        _alert = GetComponent<IAlert>();
    }
    public override void Enter()
    {
        base.Enter();
        _alert.Alert = true;
    }
    public override void Sleep()
    {
        base.Sleep();
        _alert.Alert = false;
    }
}
