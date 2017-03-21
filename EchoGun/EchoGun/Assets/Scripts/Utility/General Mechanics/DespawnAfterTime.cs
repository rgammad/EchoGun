using UnityEngine;
using System.Collections;

public class DespawnAfterTime : MonoBehaviour, ISpawnable {

    [SerializeField]
    protected float timeToLive = 1;

    float dieTime;

    void ISpawnable.Spawn() {
        dieTime = Time.time + timeToLive;
    }
    
    void Update() {
        if (Time.time > dieTime) {
            SimplePool.Despawn(transform.root.gameObject);
        }
    }
}
