using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EchoGunAPI))]
public class EchoGun : MonoBehaviour {

	PlayerMovement playerMovement;
	EchoGunAPI egAPI;
	public WeaponType weapType;
	GameObject muzzle;
	GameObject flash1;
	GameObject flash2;

	//enum for different types of weapons
	public enum eGun
	{
		STANDARD,
		PROJECTILE,
	}

	void Start()
	{
		egAPI = GetComponent<EchoGunAPI>();
		playerMovement = GetComponentInParent<PlayerMovement>();
		muzzle = GameObject.Find ("MuzzlePoint");
		flash1 = muzzle.transform.FindChild ("muzzle-flash-1").gameObject;
		flash2 = muzzle.transform.FindChild ("muzzle-flash-2").gameObject;
	}

	public eGun currentEchoType;    

	void Update()
	{

		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			Shoot();
		}
	}


	private void Shoot()
	{
		switch (currentEchoType)
		{
		case eGun.STANDARD:
			muzzleFlash(eGun.STANDARD);
			egAPI.echoStandard(weapType,playerMovement.rawAimingInput);
			break;
		case eGun.PROJECTILE:
			muzzleFlash(eGun.PROJECTILE);
			egAPI.echoProjectile(weapType,playerMovement.rawAimingInput);
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
