using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Collider2D))]
public class Bonk : MonoBehaviour
{

    public float soundRange = 5.0f;

    private void Start() {
        GetComponent<Health>().onDamage += Bonk_onDamage;
    }

    private void Bonk_onDamage(float amount) {
        Debug.Log("Hit");
        PlayerPing.CreatePing(transform.position, soundRange);
    }

    void OnCollisionEnter2D(Collision2D other) {
        //we're on a Wall-only layer; if we hit something, it's a wall
        GetComponent<playerSounds>().playBonk();
        PlayerPing.CreatePing(other.contacts[0].point, soundRange);
    }
}
