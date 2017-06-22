using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour {
    private float speedMultiplier;

	// Use this for initialization
	void Start () {
        speedMultiplier = GameScript.timer / 100.0f + 1;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x - Time.deltaTime*10.0f*speedMultiplier, transform.position.y, transform.position.z);
        if (transform.position.x <= -10.0f)
        {
            Destroy(gameObject);
        }
	}
}
