using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBehavior : MonoBehaviour {

    Health health;

    void Start()
    {
        health = GetComponentInParent<Health>();
        health.onDeath += Health_onDeath;
        health.onDamage += Health_onDamage;
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
