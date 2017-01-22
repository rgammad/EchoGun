using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatlineProjectileController : MonoBehaviour, ISpawnable {
    public int projSpeed = 10;
    public float projDamage = 10f;

    [SerializeField]
    protected GameObject deathEffectWall;

    [SerializeField]
    protected GameObject deathEffectFlesh;

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Wall")) {
            SimplePool.Despawn(this.gameObject);
            SimplePool.Spawn(deathEffectWall);
        }
        else if (other.gameObject.CompareTag("Player")) {
            other.transform.root.GetComponent<Health>().Damage(projDamage);
            Camera.main.GetComponent<CameraShakeScript>().screenShake(0.7f, 0.5f);
            SimplePool.Despawn(this.gameObject);
            SimplePool.Spawn(deathEffectFlesh);
        }
    }

    void ISpawnable.Spawn() {
        GetComponent<Rigidbody2D>().velocity = transform.right * projSpeed;
    }
}
