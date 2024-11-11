using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private bool _alreadyApllyForce;

    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_stateMachine.Enemy.AnimationData.AttackParameterHash);
        StartAnimation(_stateMachine.Enemy.AnimationData.BaseAttackParameterHash);
        _alreadyApllyForce = false;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Enemy.AnimationData.AttackParameterHash);
        StopAnimation(_stateMachine.Enemy.AnimationData.BaseAttackParameterHash);
    }

    public override void Update()
    {
        base.Update();

        ForceMove();

        float normalizedTime = GetNormalizedTime(_stateMachine.Enemy.Animator, "Attack");
        if (normalizedTime < 1f)
        {
            if (normalizedTime >= _stateMachine.Enemy.Data.ForceTransitionTime)
            {
                // ¥Ô«Œ Ω√µµ
                TryApplyForce();
            }
        }
        else
        {
            if(IsInChasingRange())
            {
                _stateMachine.ChangeState(_stateMachine.ChasingState);
                return;
            }
            else
            {
                _stateMachine.ChangeState(_stateMachine.IdleState);
                return;
            }
        }
    }

    private void TryApplyForce()
    {
        if (_alreadyApllyForce) return;
        _alreadyApllyForce = true;

        _stateMachine.Enemy.ForceReceiver.Reset();
        _stateMachine.Enemy.ForceReceiver.AddForce(Vector3.forward * _stateMachine.Enemy.Data.Force);
    }
}
