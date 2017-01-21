﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour, ISpawnable
{

    public int projSpeed = 10;
    public float soundRange = 5.0f;
    public WeaponType weapType;


    void OnCollisionEnter2D(Collision2D other)
    {
        switch (weapType)
        {
            case (WeaponType.WEAPON_STANDARD):
                if (other.gameObject.CompareTag("Wall"))
                {
                    PlayerPing.CreatePing(other.contacts[0].point, soundRange);
                    SimplePool.Despawn(this.gameObject);
                }
                break;
            case (WeaponType.WEAPON_EXPLOSION):
                if (other.gameObject.CompareTag("Wall"))
                {
                    PlayerPing.CreatePing(other.contacts[0].point, soundRange);
                    SimplePool.Despawn(this.gameObject);
                }
                break;
        }
    }

    void ISpawnable.Spawn()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * projSpeed;
    }
}
