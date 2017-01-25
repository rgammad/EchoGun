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
    [SerializeField]
    protected Vector2 shotOffset;
    [SerializeField]
    protected float speed = 5;
    [SerializeField]
    protected GameObject deathEffects;
    [SerializeField]
    protected float patrolSpeed = 3;

    Rigidbody2D rigid;
    List<Navigation.Coordinate2> pathWaypoints;
    Health health;
    Navigation navigation;
    LayerMask stageBoundary;

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
        enemyLaser = transform.root.FindChild("EnemyLaserPrefab").gameObject;
        laserRender = enemyLaser.GetComponent<LineRenderer>();
        enemyLaser.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").transform;

        rigid = GetComponentInParent<Rigidbody2D>();
        health = GetComponentInParent<Health>();
        health.onDeath += Health_onDeath;
        navigation = GetComponent<Navigation>();

        currentState = state.PATROLLING;
        firingTimer = 0;

        stageBoundary = LayerMask.GetMask("StageBoundary");
    }
	
	// Update is called once per frame
	void Update () {
        switch (currentState) {
            case state.FIRING:
                if (firingTimer > 0) {
                    if (!GetComponentInParent<SniperSound>().WasPlayed())
                    {
                        if (Random.Range(0,4) == 0)
                            GetComponentInParent<SniperSound>().playAiming();
                        GetComponentInParent<SniperSound>().toggleWasPlayed(true);
                    }
                    //Draw line toward target
                    enemyLaser.SetActive(true);
                    lockPosition = player.position;
                    laserRender.SetPosition(0, transform.position);
                    laserRender.SetPosition(1, lockPosition);
                    firingTimer -= Time.deltaTime;
                }
                else {
                    //Fire
                    GetComponentInParent<SniperSound>().playSniperFire();
                    enemyFlatlineProjectile = SimplePool.Spawn(enemyFlatlinePrefab, transform.TransformPoint(shotOffset), (lockPosition - (Vector2)transform.position).ToRotation());
                    laserRender.SetPosition(0, transform.position);
                    GetComponentInParent<SniperSound>().toggleWasPlayed(false);
                    currentState = state.WAITING;
                }
                break;
            case state.WAITING:
                laserRender.SetPosition(0, transform.position);
                if (!enemyFlatlineProjectile.activeSelf) {
                    //switch state to patrol
                    enemyLaser.SetActive(false);
                    currentState = state.PATROLLING;
                }
                break;
            case state.PATROLLING:
                rigid.MovePosition(transform.position + (transform.up * patrolSpeed * Time.deltaTime));
                ////ensure we have a path
                //while (pathWaypoints == null || pathWaypoints.Count == 0) {
                //    Navigation.Coordinate2 start = navigation.VectorToCoordinate(transform.position);
                //    Navigation.Coordinate2 destination = new Navigation.Coordinate2(Random.Range(0, Navigation.navigationWidth), Random.Range(0, Navigation.navigationWidth));

                //    //ensure path isn't too long, and destination is in the stage

                //    while (Physics2D.OverlapPoint(destination.toVector2(), stageBoundary) == null || !navigation.navigationPointWalkable(destination)) {// || (destination.toVector2() - (Vector2)this.transform.position).magnitude > 50) {
                //        destination = new Navigation.Coordinate2(Random.Range(0, Navigation.navigationWidth), Random.Range(0, Navigation.navigationWidth));
                //    }


                //    pathWaypoints = navigation.pathToPlayer(start, destination);
                //    /*
                //     * For path debugging
                //     * 
                //    if(pathWaypoints != null) {
                //        LineRenderer rend = GetComponent<LineRenderer>();
                //        rend.numPositions = pathWaypoints.Count;
                //        for(int i = 0; i < pathWaypoints.Count; i++) {
                //            rend.SetPosition(i, pathWaypoints[i].toVector2());
                //        }
                //    }
                //    */
                //}
                //rigid.MovePosition(Vector2.MoveTowards(transform.position, pathWaypoints[0].toVector2(), speed * Time.deltaTime));
                //if (Vector2.Distance(transform.position, pathWaypoints[0].toVector2()) < 0.33f) {
                //    pathWaypoints.RemoveAt(0);
                //}
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D trigger) {
        if (currentState==state.PATROLLING && trigger.tag == "Sound Trigger") {
            //Check in an area around it
            GetComponentInParent<SniperSound>().playWhatWasThat();
            Collider2D player = Physics2D.OverlapCircle(trigger.transform.position, visionCheckRadius, LayerMask.GetMask("Player"));
            //If player, do the thing
            if (player != null) {
                currentState = state.FIRING;
                firingTimer = lockOnTime;
            }
        }
    }

    private void Health_onDeath() {
        GetComponentInParent<SniperSound>().playDeath();
        Camera.main.GetComponent<CameraShakeScript>().screenShake(0.7f, 0.3f);
        enemyLaser.SetActive(false);
        Destroy(transform.root.gameObject);
        health.onDeath -= Health_onDeath;
        SimplePool.Spawn(deathEffects);
    }
}
