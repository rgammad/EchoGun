using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SimpleRollerAI : MonoBehaviour {
    Health health;

    Rigidbody2D rigid;
	float maxSpeed = 5;
	float accel = 4;
	float rotationSpeed = 0.2f;

	Vector2 targetPos;
	bool following = false;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D>();
        health = GetComponentInParent<Health>();
        health.onDeath += Health_onDeath;
        health.onDamage += Health_onDamage;
    }

   

    // Update is called once per frame
    void Update () {
		int pingCount = (int) Shader.GetGlobalFloat (Tags.ShaderParams.globalPingCount);
		if (pingCount > 0) {
			targetPos = Shader.GetGlobalVectorArray (Tags.ShaderParams.globalPingPos) [0]; //follow the most recent ping
			following = true;
			Debug.Log (targetPos);
		}

		if (following) {
			//rigid.velocity = Vector2.ClampMagnitude (Vector2.MoveTowards (rigid.velocity, maxSpeed * rigid.velocity.normalized, maxSpeed * accel * Time.deltaTime), maxSpeed);
			Vector2 move = targetPos - (Vector2) transform.position;
			rigid.velocity = Vector2.ClampMagnitude (Vector2.MoveTowards (rigid.velocity, maxSpeed * move, maxSpeed * accel * Time.deltaTime), maxSpeed);
			Debug.Log ("Veloctity: " + rigid.velocity);
		}
	}

	void FixedUpdate() {
		if (following) {
			rigid.MoveRotation (Quaternion.Slerp (transform.rotation, targetPos.ToRotation (), rotationSpeed).eulerAngles.z);
		}
	}


    private void Health_onDamage(float amount, int playerID)
    {
        PlayerPing.CreatePing(transform.position, 1.0f);
    }

    private void Health_onDeath()
    {
        //temporary until universal ping is created?
        PlayerPing.CreatePing(transform.position, 2.5f);
        Destroy(gameObject);
        health.onDeath -= Health_onDeath;
    }
}
