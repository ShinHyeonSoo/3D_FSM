using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;

    [SerializeField] private float _drag = 0.3f;
    private float _verticalVelocity;

    public Vector3 Movement => _impact + Vector3.up * _verticalVelocity;
    private Vector3 _dampingVelocity;
    private Vector3 _impact;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_controller.isGrounded)
        {
            _verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            _verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        // _impact에서 Vector3.zero로 서서히 변함
        _impact = Vector3.SmoothDamp(_impact, Vector3.zero, ref _dampingVelocity, _drag);
    }

    public void Reset()
    {
        _verticalVelocity = 0;
        _impact = Vector3.zero;
    }

    public void AddForce(Vector3 force)
    {
        _impact += force;
    }

    public void Jump(float jumpForce)
    {
        _verticalVelocity += jumpForce;
    }
}
