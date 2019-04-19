using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    Rigidbody2D _rb;
    float _xInput, _yInput;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _xInput = Input.GetAxis("L_Horizontal");
        _yInput = Input.GetAxis("L_Vertical");
    }

    private void FixedUpdate()
    {
        _rb.AddForce(300 * new Vector2(_xInput, _yInput));
    }
}
