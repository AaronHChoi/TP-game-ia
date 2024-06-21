using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerStateWalk<T> : State<T>
{
    IPlayerModel _player;
    T _idleInput;
    public PlayerStateWalk(IPlayerModel player, /*IPlayerView view,*/ T idleInput)
    {
        _player = player;
        _idleInput = idleInput;
    }
    public override void Execute()
    {
        base.Execute();

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(x, 0f, z).normalized;

        _player.Move(dir);
        _player.LookDir(dir);

        if (x == 0 && z == 0)
        {
            _fsm.Transition(_idleInput);
        }
    }
}
