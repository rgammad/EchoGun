using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonk : MonoBehaviour
{

    public float soundRange = 5.0f;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            GetComponent<playerSounds>().playBonk();
            PlayerPing.CreatePing(other.contacts[0].point, soundRange);
        }
    }
}
