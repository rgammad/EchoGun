using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Destructible"))
        {
            GetComponent<playerSounds>().playBonk();
            PlayerPing.CreatePing(other.contacts[0].point, soundRange);
        }
    }
}
