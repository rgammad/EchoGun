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
        Health targetHealth = other.transform.root.GetComponent<Health>();
        if(targetHealth != null) {
            //if the target is damageable
            targetHealth.Damage(projDamage);
            SimplePool.Spawn(deathEffectFlesh);
        } else {
            //target isn't damageable
            SimplePool.Spawn(deathEffectWall, transform.position);
        }

        SimplePool.Despawn(this.gameObject);
    }

    void ISpawnable.Spawn() {
        GetComponent<Rigidbody2D>().velocity = transform.right * projSpeed;
    }
}