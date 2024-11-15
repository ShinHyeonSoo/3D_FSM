using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [SerializeField] private string _groundParameterName = "@Ground";
    [SerializeField] private string _idleParameterName = "Idle";
    [SerializeField] private string _walkParameterName = "Walk";
    [SerializeField] private string _runParameterName = "Run";

    [SerializeField] private string _airParameterName = "@Air";
    [SerializeField] private string _jumpParameterName = "Jump";
    [SerializeField] private string _fallParameterName = "Fall";

    [SerializeField] private string _attackParameterName = "@Attack";
    [SerializeField] private string _comboAttackParameterName = "ComboAttack";

    [SerializeField] private string _baseAttackParameterName = "BaseAttack";

    public int GroundParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }

    public int AirParameterHash { get; private set; }
    public int JumpParameterHash { get; private set; }
    public int FallParameterHash { get; private set; }

    public int AttackParameterHash { get; private set; }
    public int ComboAttackParameterHash { get; private set; }

    public int BaseAttackParameterHash { get; private set; }

    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(_groundParameterName);
        IdleParameterHash = Animator.StringToHash(_idleParameterName);
        WalkParameterHash = Animator.StringToHash(_walkParameterName);
        RunParameterHash = Animator.StringToHash(_runParameterName);

        AirParameterHash = Animator.StringToHash(_airParameterName);
        JumpParameterHash = Animator.StringToHash(_jumpParameterName);
        FallParameterHash = Animator.StringToHash(_fallParameterName);

        AttackParameterHash = Animator.StringToHash(_attackParameterName);
        ComboAttackParameterHash = Animator.StringToHash(_comboAttackParameterName);

        BaseAttackParameterHash = Animator.StringToHash(_baseAttackParameterName);
    }
}
