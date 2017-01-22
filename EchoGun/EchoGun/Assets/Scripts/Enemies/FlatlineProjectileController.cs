using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatlineProjectileController : MonoBehaviour, ISpawnable {
    public int projSpeed = 10;
    public float projDamage = 10f;

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Wall")) {
            SimplePool.Despawn(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Player")) {
            other.transform.root.GetComponent<Health>().Damage(projDamage);
            Camera.main.GetComponent<CameraShakeScript>().screenShake(0.5f);
            SimplePool.Despawn(this.gameObject);
        }
    }

    void ISpawnable.Spawn() {
        GetComponent<Rigidbody2D>().velocity = transform.right * projSpeed;
    }
}
