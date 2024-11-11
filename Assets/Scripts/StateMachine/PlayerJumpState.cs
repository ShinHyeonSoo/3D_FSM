using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.JumpForce = _stateMachine.Player.Data.AirData.JumpForce;
        _stateMachine.Player.ForceReceiver.Jump(_stateMachine.JumpForce);
        base.Enter();
        StartAnimation(_stateMachine.Player.AnimationData.JumpParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Player.AnimationData.JumpParameterHash);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if(_stateMachine.Player.Controller.velocity.y <= 0)
        {
            _stateMachine.ChangeState(_stateMachine.FallState);
            return;
        }
    }
}