using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    IPlayerModel _player;
    //IPlayerView _view;
    Rigidbody _rb;
    [SerializeField] Animator _anim;
    FSM<StatesEnum> _fsm;
    private void Awake()
    {
        _player = GetComponent<IPlayerModel>();
        //_view = GetComponent<IPlayerView>();
        _rb = GetComponent<Rigidbody>();
        InitializeFSM();
    }
    void InitializeFSM()
    {
        _fsm = new FSM<StatesEnum>();

        var idle = new PlayerStateIdle<StatesEnum>(StatesEnum.Walk);
        var walk = new PlayerStateWalk<StatesEnum>(_player/*, _view*/, StatesEnum.Idle);

        idle.AddTransition(StatesEnum.Walk, walk);
        walk.AddTransition(StatesEnum.Idle, idle);

        _fsm.SetInit(idle);
    }
    void Update()
    {
        _fsm.OnUpdate();
        if (_rb.velocity.x != 0 || _rb.velocity.z != 0)
        {
            _anim.SetBool("isWalking", true);
        }
        else
        {
            _anim.SetBool("isWalking", false);
        }
    }
    public void ChangeModel(IPlayerModel model)
    {
        _player = model;
    }
}
