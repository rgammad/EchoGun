using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class EnemyProjectileController : MonoBehaviour, ISpawnable {

    [SerializeField]
    protected int projSpeed = 20;
    [SerializeField]
    protected float projDamage = 10f;

    [SerializeField]
    protected GameObject deathEffectWall;

    [SerializeField]
    protected GameObject deathEffectFlesh;


    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Wall")) {
            SimplePool.Despawn(this.gameObject);
            SimplePool.Spawn(deathEffectWall, transform.position);
        }
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Destructible")) {
            other.transform.root.GetComponent<Health>().Damage(projDamage);
            SimplePool.Despawn(this.gameObject);
            SimplePool.Spawn(deathEffectFlesh);
        }
    }

    void ISpawnable.Spawn() {
        GetComponent<Rigidbody2D>().velocity = transform.right * projSpeed;
    }
}