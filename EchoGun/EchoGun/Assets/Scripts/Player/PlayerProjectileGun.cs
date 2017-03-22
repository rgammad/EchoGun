using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public interface IRateLimited {
    void ResetLastActivationTime();
}

public class PlayerProjectileGun : MonoBehaviour, IRateLimited {

    [SerializeField]
    protected GameObject muzzleFXPrefab;
    [SerializeField]
    protected GameObject bulletPrefab;

    [SerializeField]
    protected Transform muzzlePosition;

    [SerializeField]
    protected float shotCycleTime = 0.05f;
    [SerializeField]
    protected float burstReloadTime = 0.2f;
    [SerializeField]
    protected int shotsPerBurst = 3;

    [SerializeField]
    protected float spreadDegrees = 10;
    
    [SerializeField]
    protected GameObject muzzleSoundRenderingPrefab;

    private Animator anim;

    FXComponent[] muzzleFXComponents;

    float previousShotTime = 0;
    int shotsRemainingInBurst;

    void Start() {
        anim = GetComponentInChildren<Animator>();

        shotsRemainingInBurst = shotsPerBurst;

        GameObject instantiatedMuzzleFX = Instantiate(muzzleFXPrefab, muzzlePosition);
        muzzleFXComponents = instantiatedMuzzleFX.GetComponentsInChildren<FXComponent>();
    }

    void Update() {
        if (Input.GetKey(KeyCode.Mouse0) && canShoot()) {
            Shoot();

            anim.SetBool("isShooting", true);
        } else {
            anim.SetBool("isShooting", false);
        }
    }

    bool canShoot() {
        if(shotsRemainingInBurst == 0) {
            //use burst reload time
            if (Time.time > previousShotTime + burstReloadTime) {
                //we've reloaded the burst
                shotsRemainingInBurst = shotsPerBurst;
            } else {
                return false;
            }
        }

        if(shotsRemainingInBurst > 0) {
            //use burst time
            return Time.time > previousShotTime + shotCycleTime;
        }

        return false;
    }

    private void Shoot() {
        previousShotTime = Time.time;
        shotsRemainingInBurst--;

        SimplePool.Spawn(muzzleSoundRenderingPrefab, transform.position);

        foreach(FXComponent muzzleFXComponent in muzzleFXComponents) {
            muzzleFXComponent.TriggerEffect();
        }

        Vector2 initialPos = transform.position;
        Vector2 targetPos = Format.mousePosInWorld();

        Quaternion targetRotation = (targetPos - initialPos).ToRotation();
        targetRotation = targetRotation * Quaternion.AngleAxis(spreadDegrees * ((2 * Random.value) - 1), Vector3.forward);

        Camera.main.GetComponent<CameraShakeScript>().screenShake(0.1f, 0.1f);

        SimplePool.Spawn(bulletPrefab, muzzlePosition.position, targetRotation);
    }

    void IRateLimited.ResetLastActivationTime() {
        previousShotTime = Time.time;
    }
}
