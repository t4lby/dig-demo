using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public float JumpVelocity;
    public float MoveVelocity;
    public GameObject HealthBar;
    public string PlayerNumber;
    Rigidbody2D _rb;
    float _xInput, _yInput;
    bool _jump;
    bool _collisionBelow;
    int _framesSinceNoCollision;
    float _health;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _health = 10f;
    }

    private void Update()
    {
        _xInput = Input.GetAxis("L_Horizontal_" + PlayerNumber);
        _yInput = Input.GetAxis("L_Vertical_" + PlayerNumber);
        _jump = Input.GetButton("Jump_" + PlayerNumber);
        if (_health < 0)
        {
            Destroy(this.gameObject);
        }
        HealthBar.transform.localScale = new Vector3(_health / 10, 1, 1);
    }

    private void FixedUpdate()
    {
        //_rb.AddForce(MoveForce * new Vector2(_xInput, _yInput));
        _rb.velocity = new Vector2(_xInput * MoveVelocity, (_jump && _collisionBelow) ? JumpVelocity : _rb.velocity.y);
        if (_framesSinceNoCollision > 5)
        {
            _collisionBelow = false;
        }
        _framesSinceNoCollision += 1;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (transform.position.y > collision.transform.position.y &&
            Mathf.Abs(transform.position.x - collision.transform.position.x) < 1)
        {
            _collisionBelow = true;
            _framesSinceNoCollision = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "dirt")
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "shovel" && collision.GetComponentInParent<Character>() != this)
        {
            _health -= 3f;
            // Turn sprite color to red.
        }
    }
}
