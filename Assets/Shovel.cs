using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Shovel : MonoBehaviour 
{
    /// <summary>
    /// The shovel retracted distance. X-value of shovel when back.
    /// </summary>
    public float ShovelRetractedDistance = -1f;
    /// <summary>
    /// The shovel extended distance. X-value of shovel when outstreched.s
    /// </summary>
    public float ShovelExtendedDistance = 1f;
    /// <summary>
    /// The amount of force used to extend the shovel.
    /// </summary>
    public float ShovelExtensionForceModifier = 100f;
    /// <summary>
    /// The amount of torque used in rotating the shovel.
    /// </summary>
    public float TorqueModifier = 100f;
    /// <summary>
    /// Torque will max out to stop shovel moving through dirt.
    /// </summary>
    public float MaxTorque = 100f;

    Rigidbody2D _rbRot;
    Rigidbody2D _rbExt;
    float _xInput, _yInput;
    

    void Start () 
    {
        _rbRot = GetComponent<Rigidbody2D>();
        _rbExt = transform.Find("Extension").GetComponent<Rigidbody2D>();
    }

	void Update () 
    {
        //Get input from r_horizontal and r_vertical axis.s
        _xInput = Input.GetAxis("Horizontal_" + GetComponentInParent<Character>().PlayerNumber);
        _yInput = Input.GetAxis("Vertical_" + GetComponentInParent<Character>().PlayerNumber);
    }

    private void FixedUpdate()
    {
        //Update shovels rotation if input is large enough.
        if (new Vector2(_xInput, _yInput).magnitude > 0.1f)
        {
            float angle = 0f;
            if (Mathf.Abs(_xInput) < 0.0001f)
            {
                angle = _yInput < 0f ? -90f : 90f;
            }
            else
            {
                angle = Mathf.Rad2Deg * Mathf.Atan(_yInput / _xInput);
                    //+ (_xInput < 0f ? 1f : 0f) * 180f;
                if (_xInput < 0)
                {
                    angle = (_yInput < 0f ? -1f : 1f) * 180 + angle;
                }
            }
            //Debug.Log(cleanLocalAngle);
            _rbRot.MoveRotation(angle);
            /*float cleanLocalAngle = _rbRot.transform.localEulerAngles.z > 180 ? _rbRot.transform.localEulerAngles.z - 360 : _rbRot.transform.localEulerAngles.z;
            float torque = (angle - cleanLocalAngle) * TorqueModifier;
            if (Mathf.Abs(torque) > MaxTorque)
            {
                torque = torque > 0 ? MaxTorque : -MaxTorque;
            }
            _rbRot.AddTorque(torque);*/
        }
        float extension = ShovelRetractedDistance +
            new Vector2(_xInput, _yInput).magnitude * (ShovelExtendedDistance - ShovelRetractedDistance);
        Vector2 extensionForce = _rbExt.transform.TransformDirection(
            new Vector2(extension - _rbExt.transform.localPosition.x,
            0));
        _rbExt.AddForce(extensionForce * ShovelExtensionForceModifier);
        _rbExt.transform.localPosition = new Vector3(_rbExt.transform.localPosition.x, 0, 0);
        _rbRot.transform.localPosition = Vector3.zero;
        _rbExt.transform.localEulerAngles = Vector3.zero;
    }
}
