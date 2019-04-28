using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public float JumpVelocity;
    public float MoveSpeed;
    public float MoveAccForce;
    public float KnockBackForce;
    public GameObject HealthBar;
    public string PlayerNumber;
    public Shovel Shovel;
    Rigidbody2D _rb;
    float _xInput;
    float _digInput;
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
        _digInput = Input.GetAxis("L_Trig_" + PlayerNumber);
        _jump = Input.GetButton("Jump_" + PlayerNumber);
        Shovel.ParticleRetention = _digInput * 0.7f + 0.5f;
        if (_health < 0)
        {
            Destroy(this.gameObject);
        }
        HealthBar.transform.localScale = new Vector3(_health / 10, 1, 1);
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(_rb.velocity.x) < Mathf.Abs(_xInput * MoveSpeed))
        {
            _rb.AddForce(new Vector2(_xInput * MoveAccForce, 0));
        }
        _rb.velocity = new Vector2(_rb.velocity.x, (_jump && _collisionBelow) ? JumpVelocity : _rb.velocity.y);
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
            _health -= 1f;
            //knockback
            this.GetComponent<Rigidbody2D>().AddForce((Vector2)(transform.position - collision.transform.position).normalized * KnockBackForce);
        }
    }
}
