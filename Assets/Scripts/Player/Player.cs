using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerSO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Animator Animator { get; private set; }
    public PlayerController Input { get; private set; }
    public CharacterController Controller { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }

    private PlayerStateMachine _stateMachine;

    private void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerController>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();

        _stateMachine = new PlayerStateMachine(this);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _stateMachine.ChangeState(_stateMachine.IdleState);
    }

    private void Update()
    {
        _stateMachine.HandleInput();
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.PhysicsUpdate();
    }
}
