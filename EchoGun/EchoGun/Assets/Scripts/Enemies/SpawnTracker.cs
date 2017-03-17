using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class SpawnTracker : MonoBehaviour {

    public Spawner source;
    public float weight;

    Health health;

	void Start () {
        health = GetComponent<Health>();
        health.onDeath += SpawnTracker_onDeath;
    }

    private void SpawnTracker_onDeath() {
        source.EnemyDeath(weight);
        health.onDeath -= SpawnTracker_onDeath;
    }
}
