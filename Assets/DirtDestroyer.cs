using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtDestroyer : MonoBehaviour {

    public float MinYVal;
	
	// Update is called once per frame
	void Update ()
    {
		if (transform.position.y < MinYVal)
        {
            Destroy(this);
        }
	}
}
