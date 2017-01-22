using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
public class StormtrooperAI : MonoBehaviour {

    [SerializeField]
    protected GameObject bulletPrefab;

    [SerializeField]
    protected float speed = 5;

    /// <summary>
    /// Time at which we will stop or stopped firing.
    /// </summary>
    [SerializeField]
    protected float firingDuration = 3;
    float firingEndTime = 0;

    Health health;
    Rigidbody2D rigid;

    Vector2 targetPos;

    [SerializeField]
    protected float timePerShot = 0.1f;
    float nextShotTime = 0;

    [SerializeField]
    protected float shotSpread = 10f;

    // Use this for initialization
    void Start() {
        rigid = GetComponent<Rigidbody2D>();
        health = GetComponentInParent<Health>();

        health.onDeath += Health_onDeath;
        //health.onDamage += Health_onDamage;
    }

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

                Quaternion targetRotation = (targetPos - (Vector2)transform.position).ToRotation();
                targetRotation = targetRotation * Quaternion.AngleAxis(shotSpread * ((2 * Random.value) - 1), Vector3.forward);

                SimplePool.Spawn(bulletPrefab, this.transform.position, targetRotation);

            }

        }
        else {
            //random walk
        }

        //rigid.velocity = Vector2.ClampMagnitude(Vector2.MoveTowards(rigid.velocity, speed * (targetPos - (Vector2)transform.position + new Vector2(x, y)), maxSpeed * accel * Time.deltaTime), maxSpeed);
    }

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.tag == "Sound Trigger") {
            targetPos = trigger.transform.position;
            firingEndTime = Time.time + firingDuration;
            nextShotTime = Time.time;
        }
    }

    /*
    private void Health_onDamage(float amount, int playerID) {
        PlayerPing.CreatePing(transform.position, 1.0f);
    }
    */

    private void Health_onDeath() {
        //temporary until universal ping is created?
        //PlayerPing.CreatePing(transform.position, 2.5f);
        Destroy(gameObject);
        health.onDeath -= Health_onDeath;
    }
}
