using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtParticle : MonoBehaviour {

    public int FramesUntilSettle = 50;
    int _frameCount;

	// Use this for initialization
	void Start () 
    {
        _frameCount = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        _frameCount += 1;
        if (_frameCount > FramesUntilSettle && 
            GetComponent<Rigidbody2D>().velocity.magnitude < 0.01f)
        {
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position, 
                new Vector2(0, -1), 
                distance: 10, 
                layerMask: LayerMask.GetMask("dirtGrid"));
            if (hit.collider != null && Random.Range(0, 2) == 0)
            {
                var d = hit.collider.GetComponent<Dirt>();
                d.Map.AddDirt(d.MapIndexA, d.MapIndexB - 1);
            }
            Destroy(this.gameObject);
        }
    }
}
