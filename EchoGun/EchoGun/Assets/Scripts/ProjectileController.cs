using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour, ISpawnable
{

    public int projSpeed = 10;



    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            SimplePool.Despawn(this.gameObject);
        }
    }

    void ISpawnable.Spawn()
    {

        GetComponent<Rigidbody2D>().velocity = transform.right * projSpeed;
    }
}
