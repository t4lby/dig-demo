using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float JumpForce;
    public float MoveForce;
    Rigidbody2D _rb;
    float _xInput, _yInput;
    bool _jump;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _xInput = Input.GetAxis("L_Horizontal");
        _yInput = Input.GetAxis("L_Vertical");
        _jump = Input.GetKey(KeyCode.Space);
    }

    private void FixedUpdate()
    {
        _rb.AddForce(MoveForce * new Vector2(_xInput, _yInput));
        if (_jump) _rb.AddForce(new Vector2(0, JumpForce));
    }
}
