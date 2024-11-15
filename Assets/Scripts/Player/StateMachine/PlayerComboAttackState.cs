using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboAttackState : PlayerAttackState
{
    private bool _alreadyAppliedCombo;
    private bool _alreadyApllyForce;

    AttackInfoData _attackInfoData;

    public PlayerComboAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_stateMachine.Player.AnimationData.ComboAttackParameterHash);

        _alreadyAppliedCombo = false;
        _alreadyApllyForce = false;

        int comboIndex = _stateMachine.ComboIndex;
        _attackInfoData = _stateMachine.Player.Data.AttackData.GetAttackInfoData(comboIndex);
        _stateMachine.Player.Animator.SetInteger("Combo", comboIndex);
    }

    public override void Exit() 
    { 
        base.Exit();
        StopAnimation(_stateMachine.Player.AnimationData.ComboAttackParameterHash);

        if(!_alreadyAppliedCombo)
        {
            _stateMachine.ComboIndex = 0;
        }
    }

    public override void Update()
    {
        base.Update();

        ForceMove();

        float normalizedTime = GetNormalizedTime(_stateMachine.Player.Animator, "Attack");
        if(normalizedTime < 1f)
        {
            if(normalizedTime >= _attackInfoData.ComboTransitionTime)
            {
                // �޺� �õ�
                TryComboAttack();
            }

            if(normalizedTime >= _attackInfoData.ForceTransitionTime)
            {
                // ���� �õ�
                TryApplyForce();
            }
        }
        else
        {
            if(_alreadyAppliedCombo)
            {
                _stateMachine.ComboIndex = _attackInfoData.ComboStateIndex;
                _stateMachine.ChangeState(_stateMachine.ComboAttackState);
            }
            else
            {
                _stateMachine.ChangeState(_stateMachine.IdleState);
            }
        }
    }

    private void TryComboAttack()
    {
        if (_alreadyAppliedCombo) return;

        if (_attackInfoData.ComboStateIndex == -1) return;

        if(!_stateMachine.IsAttacking) return;

        _alreadyAppliedCombo = true;
    }

    private void TryApplyForce()
    {
        if (_alreadyApllyForce) return;
        _alreadyApllyForce = true;

        _stateMachine.Player.ForceReceiver.Reset();
        _stateMachine.Player.ForceReceiver.AddForce(Vector3.forward * _attackInfoData.Force);
    }
}
