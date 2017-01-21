using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EchoGunAPI))]
public class EchoGun : MonoBehaviour {

    PlayerMovement playerMovement;
    EchoGunAPI egAPI;
    public WeaponType weapType;
	GameObject muzzle;

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
		muzzle = GameObject.Find ("MuzzlePoint");
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
			
			break;
		case eGun.PROJECTILE:
			
			break;
		default:
			break;
		}
	}
}
