using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Navigation))]
public class StormtrooperAI : MonoBehaviour {

    [SerializeField]
    protected GameObject bulletPrefab;

    [SerializeField]
    protected GameObject deathEffects;

    [SerializeField]
    protected float speed = 5;

    [SerializeField]
    protected float patrolSpeed = 10f;

    /// <summary>
    /// Time at which we will stop or stopped firing.
    /// </summary>
    [SerializeField]
    protected float firingDuration = 3;
    float firingEndTime = 0;

    Health health;
    Rigidbody2D rigid;
    ObjectSoundVisuals soundVisuals;
    Navigation navigation;

    LayerMask stageBoundary;

    Vector2 targetPos;

    [SerializeField]
    protected float timePerShot = 0.1f;
    float nextShotTime = 0;

    [SerializeField]
    protected float shotSpread = 10f;

    // Use this for initialization
    void Start() {
        rigid = GetComponentInParent<Rigidbody2D>();
        health = GetComponentInParent<Health>();
        navigation = GetComponent<Navigation>();

        health.onDeath += Health_onDeath;
        health.onDamage += Health_onDamage;

        stageBoundary = LayerMask.GetMask(Tags.Layers.StageBoundary);
        soundVisuals = transform.root.GetComponentInChildren<ObjectSoundVisuals>();
        Assert.IsNotNull(soundVisuals);
    }

    List<Navigation.Coordinate2> pathWaypoints;

    // Update is called once per frame
    void Update() {

        if (Time.time < firingEndTime) {
            //fire, or move to fire
            RaycastHit2D[] firingPath = Physics2D.LinecastAll(transform.position, targetPos);


            foreach(RaycastHit2D hit in firingPath) {
                if(hit.transform.CompareTag("Wall")) {
                    //cannot fire, return to normal
                    firingEndTime = 0;
                    return;
                }
            }

            while(Time.time > nextShotTime) {
                nextShotTime += timePerShot;
                //fire a shot
                GetComponentInParent<StormtrooperSound>().playBlasterSound();
                Quaternion targetRotation = (targetPos - (Vector2)transform.position).ToRotation();
                targetRotation = targetRotation * Quaternion.AngleAxis(shotSpread * ((2 * Random.value) - 1), Vector3.forward);

                SimplePool.Spawn(bulletPrefab, this.transform.position, targetRotation);

            }

        }
        else {
            rigid.MovePosition(transform.position + (transform.up * patrolSpeed * Time.deltaTime));
            ////ensure we have a path
            //while(pathWaypoints == null || pathWaypoints.Count == 0) {
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
            //rigid.MoveRotation((pathWaypoints[0].toVector2() - (Vector2)(transform.position)).ToAngle());
            //if(Vector2.Distance(transform.position, pathWaypoints[0].toVector2()) < 0.33f) {
            //    pathWaypoints.RemoveAt(0);
            //}
        }

        //rigid.velocity = Vector2.ClampMagnitude(Vector2.MoveTowards(rigid.velocity, speed * (targetPos - (Vector2)transform.position + new Vector2(x, y)), maxSpeed * accel * Time.deltaTime), maxSpeed);
    }

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.tag == "Sound Trigger") {
            targetPos = trigger.transform.position;
            firingEndTime = Time.time + firingDuration;
            nextShotTime = Time.time;
            GetComponentInParent<StormtrooperSound>().playFiringLaser();
        }
    }

    
    private void Health_onDamage(float amount) {
        soundVisuals.TriggerEffect();
    }
    

    private void Health_onDeath() {
        //temporary until universal ping is created?
        //PlayerPing.CreatePing(transform.position, 2.5f);

        GetComponentInParent<StormtrooperSound>().playDeath();
        Camera.main.GetComponent<CameraShakeScript>().screenShake(0.7f, 0.3f);
        Destroy(transform.root.gameObject);
        health.onDeath -= Health_onDeath;
        SimplePool.Spawn(deathEffects);
    }
}
