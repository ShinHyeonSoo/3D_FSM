using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_stateMachine.Player.AnimationData.FallParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Player.AnimationData.FallParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if(_stateMachine.Player.Controller.isGrounded)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
            return;
        }
    }
}
