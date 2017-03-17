using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class DestructibleTracker : MonoBehaviour {

    public Spawner source;

    Health health;

    void Start() {
        health = GetComponent<Health>();
        health.onDeath += SpawnTracker_onDeath;
    }

    private void SpawnTracker_onDeath() {
        source.DestructibleDeath();
        health.onDeath -= SpawnTracker_onDeath;
    }
}
