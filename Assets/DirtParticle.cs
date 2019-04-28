using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtParticle : MonoBehaviour {

    public int FramesUntilSettle = 100;
    public int FramesUntilColliderTurnOn = 10;
    public float DistanceTillColliderTurnOn = 2f;
    int _frameCount;
    Vector3 _initialLocation;

	// Use this for initialization
	void Start () 
    {
        _frameCount = 0;
        _initialLocation = transform.position;
        this.GetComponent<Collider2D>().enabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        _frameCount += 1;
        if (_frameCount > FramesUntilSettle && 
            GetComponent<Rigidbody2D>().velocity.magnitude < 0.1f)
        {
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position, 
                new Vector2(0, -1), 
                distance: 10, 
                layerMask: LayerMask.GetMask("dirtGrid"));
            if (hit.collider != null)
            {
                var d = hit.collider.GetComponent<Dirt>();
                d.Map.AddDirt(d.MapIndexA, d.MapIndexB - 1);
            }
            Destroy(this.gameObject);
        }
        if (_frameCount > FramesUntilColliderTurnOn || (transform.position - _initialLocation).magnitude > DistanceTillColliderTurnOn)
        {
            this.GetComponent<Collider2D>().enabled = true;
        }
    }
}
