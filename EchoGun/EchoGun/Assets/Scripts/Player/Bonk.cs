using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AudioSource))]
public class Bonk : MonoBehaviour
{

    [SerializeField]
    protected GameObject soundRenderingPrefab;

    [SerializeField]
    protected AudioClip bonkClip;

    [SerializeField]
    protected float volumeModifier = 1;

    AudioSource source;

    private void Start() {
        GetComponent<Health>().onDamage += Bonk_onDamage;
        source = GetComponent<AudioSource>();
    }

    private void Bonk_onDamage(float amount) {
        SimplePool.Spawn(soundRenderingPrefab, transform.position);
    }

    void OnCollisionEnter2D(Collision2D other) {
        //we're on a Wall-only layer; if we hit something, it's a wall
        source.PlayOneShot(bonkClip, volumeModifier);

        SimplePool.Spawn(soundRenderingPrefab, transform.position);
    }
}
