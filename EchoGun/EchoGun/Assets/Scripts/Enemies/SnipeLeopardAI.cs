using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnipeLeopardAI : MonoBehaviour {
    [SerializeField]
    protected GameObject enemyLaserPrefab;
    [SerializeField]
    protected GameObject enemyFlatlinePrefab;
    [SerializeField]
    protected float visionCheckRadius = 10;

    Health health;
    Rigidbody2D rigid;

    private enum state {
        FIRING,
        WAITING,
        PATROLLING
    }

    private state currentState;
    private GameObject enemyLaser;
    private GameObject enemyFlatlineProjectile;
    private LineRenderer laserRender;
    private float lockOnTime = 2.0f;
    private float firingTimer;
    private Vector2 lockPosition;
    private Transform player;

	// Use this for initialization
	void Start () {
        enemyLaser = (GameObject)Instantiate(enemyLaserPrefab);
        laserRender = enemyLaser.GetComponent<LineRenderer>();
        enemyLaser.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").transform;

        health = GetComponent<Health>();
        rigid = GetComponent<Rigidbody2D>();

        currentState = state.PATROLLING;
        firingTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        switch (currentState) {
            case state.FIRING:
                if (firingTimer > 0) {
                    //Draw line toward target
                    enemyLaser.SetActive(true);
                    lockPosition = player.position;
                    laserRender.SetPosition(0, transform.position);
                    laserRender.SetPosition(1, lockPosition);
                    firingTimer -= Time.deltaTime;
                }
                else {
                    //Fire
                    enemyFlatlineProjectile = (GameObject)Instantiate(enemyFlatlinePrefab);
                    currentState = state.WAITING;
                }
                break;
            case state.WAITING:
                if (enemyFlatlineProjectile == null) {
                    //switch state to patrol
                    enemyLaser.SetActive(false);
                    currentState = state.PATROLLING;
                }
                break;
            case state.PATROLLING:
                //random patrol
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.tag == "Sound Trigger") {
            //Check in an area around it
            Collider2D player = Physics2D.OverlapCircle(trigger.transform.position, visionCheckRadius, LayerMask.GetMask("Player"));
            //If player, do the thing
            if (player != null) {
                currentState = state.FIRING;
                firingTimer = lockOnTime;
            }
        }
    }
}
