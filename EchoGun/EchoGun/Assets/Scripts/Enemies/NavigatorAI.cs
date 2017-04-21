using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(NavMeshAgent))]
public class NavigatorAI : MonoBehaviour {

    Rigidbody2D rigid;
    ObjectSoundVisuals soundVisuals;
    NavMeshAgent agent;

    CircleCollider2D soundTrigger;

    Health health;

    void Start() {
        rigid = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        agent = GetComponent<NavMeshAgent>();

        soundTrigger = transform.GetChild(0).GetComponent<CircleCollider2D>();
        health.onDeath += Health_onDeath;
        health.onDamage += Health_onDamage;

        soundVisuals = GetComponentInChildren<ObjectSoundVisuals>();

    }

    private void Update() {
        if(agent.remainingDistance < 0.1f) {
            Vector3 destination;
            agent.SetPath(TwoDNavMeshBuilderWithReachability.Main.RandomPath(transform.position, out destination));
        }
    }

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.CompareTag("Sound Trigger")) {
            agent.SetDestination(trigger.transform.position);
        }
    }

    private void Health_onDamage(float amount) {
        soundVisuals.TriggerEffect();
    }

    private void Health_onDeath() {
        Destroy(this.gameObject); 
    }
}
