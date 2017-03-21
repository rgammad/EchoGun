using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBehavior : MonoBehaviour {

    Health health;
    ObjectSoundVisuals soundVisuals;

    void Start()
    {
        health = GetComponentInParent<Health>();
        health.onDeath += Health_onDeath;
        health.onDamage += Health_onDamage;
        soundVisuals = GetComponentInChildren<ObjectSoundVisuals>();
    }

    private void Health_onDamage(float amount)
    {
        soundVisuals.TriggerEffect();
    }

    private void Health_onDeath()
    {
        Destroy(gameObject);
        health.onDeath -= Health_onDeath;
    }

}
