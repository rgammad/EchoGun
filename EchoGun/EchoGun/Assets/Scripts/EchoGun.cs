﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EchoGunAPI))]
public class EchoGun : MonoBehaviour {

	PlayerMovement playerMovement;
	EchoGunAPI egAPI;

	GameObject muzzle;
	GameObject flash1;
	GameObject flash2;

    public WeaponType weapType;
    public float shootingSoundRange = 25.0f;

    private Animator anim;
    private bool isShooting = false;

	//enum for different types of weapons
	public enum eGun
	{
		STANDARD,
		PROJECTILE,
	}

	void Start()
	{
		egAPI = GetComponent<EchoGunAPI>();
		playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponentInChildren<Animator>();
		muzzle = GameObject.Find ("MuzzlePoint");
		flash1 = muzzle.transform.FindChild ("muzzle-flash-1").gameObject;
		flash2 = muzzle.transform.FindChild ("muzzle-flash-2").gameObject;
	}

	public eGun currentEchoType;    

	void Update()
	{
        UpdateAnimator();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
            isShooting = true;
        }
        else
            isShooting = false;
	}

    void UpdateAnimator()
    {
        anim.SetBool("isShooting", isShooting);
    }

	private void Shoot()
	{
        PlayerPing.CreatePing(transform.position, shootingSoundRange);
		switch (currentEchoType)
		{
		case eGun.STANDARD:
			muzzleFlash(eGun.STANDARD);
			egAPI.echoStandard(weapType);
			break;
		case eGun.PROJECTILE:
			muzzleFlash(eGun.PROJECTILE);
			egAPI.echoProjectile(weapType);
			break;
		default:
			break;
		}
	}
	private void muzzleFlash(eGun type){
		switch (type)
		{
		case eGun.STANDARD:

			flash1.SetActive (true);
			Invoke ("disableMuzzles", 0.1f);
			break;
		case eGun.PROJECTILE:
			flash2.SetActive (true);
			Invoke ("disableMuzzles", 0.1f);
			break;
		default:
			break;
		}
	}

	void disableMuzzles(){
		flash1.SetActive (false);
		flash2.SetActive (false);
	}
}
