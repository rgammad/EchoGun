using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SimpleRollerAI : MonoBehaviour {
	public float radius;
	public float explodeDist;
	public float explodeDamage;
	public float explodeWaveAmt;

	public float maxSpeed;
	float accel = 4;
	float rotationSpeed = 0.2f;

	Rigidbody2D rigid;

	Vector2 targetPos;
	bool following = false;

	CircleCollider2D soundTrigger;

	Health health;
	GameObject player;
	Animator anim;

	void Start () {
		rigid = GetComponent<Rigidbody2D>();
		health = GetComponent<Health>();
		anim = GetComponent<Animator> ();
		anim.SetBool ("Explode", false);

		soundTrigger = transform.GetChild(0).GetComponent<CircleCollider2D> ();
		soundTrigger.radius = radius;
		health.onDeath += Health_onDeath;
		health.onDamage += Health_onDamage;

		player = GameObject.FindGameObjectWithTag ("Player");

	}

	void Update () {
		//int pingCount = (int) Shader.GetGlobalFloat (Tags.ShaderParams.globalPingCount);
		//if (pingCount > 0) {
		//	targetPos = Shader.GetGlobalVectorArray (Tags.ShaderParams.globalPingPos) [0]; //follow the most recent ping
		//	following = true;
		//}

		if (Vector2.Distance (this.transform.position, player.transform.position) <= radius) {
			targetPos = player.transform.position;
			following = true;
		}

		if (following) {
			float x = Random.Range (-6f, 6f);
			float y = Random.Range (-6f, 6f);
			rigid.velocity = Vector2.ClampMagnitude (Vector2.MoveTowards (rigid.velocity, maxSpeed * (targetPos - (Vector2) transform.position + new Vector2 (x, y)), maxSpeed * accel * Time.deltaTime), maxSpeed);
		}

	}

	void FixedUpdate() {
		if (following) {
			rigid.MoveRotation (Quaternion.Slerp (transform.rotation, targetPos.ToRotation (), rotationSpeed).eulerAngles.z);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.CompareTag("Player")) {
			anim.SetBool ("Explode", true);
			GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraShakeScript> ().screenShake (.75f);
			PlayerPing.CreatePing(transform.position, explodeWaveAmt);
			player.GetComponent<Health> ().Damage (explodeDamage);
		}
	}

	public void Die() {
		Destroy (this.gameObject);
	}


	void OnTriggerEnter2D(Collider2D trigger) {
		if (trigger.CompareTag("Sound Trigger")) {
			targetPos = trigger.transform.position;
			following = true;
		}
	}

	void OnTriggerExit2D(Collider2D trigger) {
		if (trigger.CompareTag("Sound Trigger")) {
			following = false;
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
