using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour, ISpawnable
{

    [SerializeField]
    protected GameObject deathEffectWall;
    [SerializeField]
    protected GameObject deathEffectFlesh;

    [SerializeField]
    protected GameObject impactSoundRenderingPrefab;

    Rigidbody2D rigid;

    [SerializeField]
    protected float projSpeed = 10;
    [SerializeField]
    protected float projDamage = 10f;

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Wall"))
        {
            SimplePool.Spawn(impactSoundRenderingPrefab, transform.position);
            SimplePool.Spawn(deathEffectWall, transform.position);
            SimplePool.Despawn(this.gameObject);
        }
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Destructible"))
        {
            other.transform.root.GetComponent<Health>().Damage(projDamage);

            SimplePool.Spawn(impactSoundRenderingPrefab, transform.position);
            SimplePool.Spawn(deathEffectFlesh, transform.position);
            SimplePool.Despawn(this.gameObject);
        }
    }

    void ISpawnable.Spawn()
    {
        rigid.velocity = transform.right * projSpeed;
    }
}
