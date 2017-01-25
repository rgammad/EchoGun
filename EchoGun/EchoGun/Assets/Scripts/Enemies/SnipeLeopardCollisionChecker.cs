using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnipeLeopardCollisionChecker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void OnCollisionStay2D(Collision2D other) {
        if (other.collider.CompareTag("Wall")) {
            transform.root.rotation = ((Vector2)Random.insideUnitCircle.normalized).ToRotation();
        }
    }
}
