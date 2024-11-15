using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : IState
{
    protected EnemyStateMachine _stateMachine;
    protected readonly PlayerGroundData _groundData;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
        _groundData = stateMachine.Enemy.Data.GroundData;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void HandleInput()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Update()
    {
        Move();
    }

    protected void StartAnimation(int animatorHash)
    {
        _stateMachine.Enemy.Animator.SetBool(animatorHash, true);
    }

    protected void StopAnimation(int animatorHash)
    {
        _stateMachine.Enemy.Animator.SetBool(animatorHash, false);
    }

    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();

        Move(movementDirection);

        Rotate(movementDirection);
    }

    private Vector3 GetMovementDirection()
    {
        Vector3 dir = (_stateMachine.Target.transform.position - _stateMachine.Enemy.transform.position);

        return dir;
    }

    private void Move(Vector3 direction)
    {
        float movementSpeed = GetMovementSpeed();
        _stateMachine.Enemy.Controller.Move(((direction * movementSpeed) + _stateMachine.Enemy.ForceReceiver.Movement) * Time.deltaTime);
    }

    private float GetMovementSpeed()
    {
        return _stateMachine.MovementSpeed * _stateMachine.MovementSpeedModifier;
    }

    private void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Transform playerTransform = _stateMachine.Enemy.transform;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, _stateMachine.RotationDamping * Time.deltaTime);
        }
    }

    protected void ForceMove()
    {
        _stateMachine.Enemy.Controller.Move(_stateMachine.Enemy.ForceReceiver.Movement * Time.deltaTime);
    }

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        // 전환되고 있을 때 && 다음 애니메이션이 tag
        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        // 전환되고 있지 않을 때 && 현재 애니메이션이 tag
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0;
        }
    }

    protected bool IsInChasingRange()
    {
        if (_stateMachine.Target._isDie) return false;

        float playerDistanceSqr = (_stateMachine.Target.transform.position - _stateMachine.Enemy.transform.position).sqrMagnitude;
        return playerDistanceSqr <= _stateMachine.Enemy.Data.PlayerChasingRange * _stateMachine.Enemy.Data.PlayerChasingRange;
    }
}
