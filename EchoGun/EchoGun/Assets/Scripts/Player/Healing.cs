using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Health))]
public class Healing : MonoBehaviour {

    [SerializeField]
    protected float healsPerSecond = 2;

    Health health;

	void Start () {
        health = GetComponent<Health>();
    }
	
	void Update () {
        health.Heal(Time.time * healsPerSecond);
    }
}
