using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour {
    private Transform playerTransform;
    private Rigidbody2D playerRigid;
    private float defaultPlayerSpeed;
    private Vector2 velocity;

    public float playerBuffer = 5;
    public float cameraTrackDistance = .5f;
    public float trackSpeedScaling = 1.0f;

	// Use this for initialization
	void Start () {
        GameObject player = GameObject.FindGameObjectWithTag("player");
        playerTransform = player.transform;
        defaultPlayerSpeed = player.GetComponent<PlayerMovement>().MaxSpeed;
        playerRigid = player.GetComponent<Rigidbody2D>();
        velocity = new Vector2();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector2 mousePosition = Format.mousePosInWorld();
        Vector2 trackingPosition = Vector2.Lerp(mousePosition, playerTransform.position, cameraTrackDistance);
        if (Vector2.Distance(trackingPosition, playerTransform.position) > playerBuffer) {
            Vector2 aimDirection = (trackingPosition-(Vector2)playerTransform.position).normalized;
            trackingPosition = (Vector2)playerTransform.position + (aimDirection * playerBuffer);
        }
        transform.position = (Vector2)transform.position+(playerRigid.velocity*Time.fixedDeltaTime);
        transform.position = Vector2.MoveTowards(transform.position, trackingPosition, trackSpeedScaling*(Vector2.Distance(trackingPosition, transform.position)));
    }
}
