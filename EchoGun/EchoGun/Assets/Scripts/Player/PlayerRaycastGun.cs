using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerRaycastGun : MonoBehaviour {

    [SerializeField]
    protected GameObject muzzleFXPrefab;

    [SerializeField]
    protected GameObject echoStandardPrefab;

    [SerializeField]
    protected Transform muzzlePosition;

    [SerializeField]
    protected float shotCycleTime = 0.5f;

    [SerializeField]
    protected float damage = 100f;

    [SerializeField]
    protected GameObject muzzleSoundRenderingPrefab;

    private Animator anim;
    LayerMask wall;

    FXComponent[] muzzleFXComponents;

    float previousShotTime = 0;

    void Start() {
        anim = GetComponentInChildren<Animator>();

        GameObject instantiatedMuzzleFX = Instantiate(muzzleFXPrefab, muzzlePosition);
        muzzleFXComponents = instantiatedMuzzleFX.GetComponentsInChildren<FXComponent>();

        wall = LayerMask.GetMask("Wall");
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
        return Time.time > previousShotTime + shotCycleTime;
    }

    private void Shoot() {
        previousShotTime = Time.time;

        foreach (FXComponent muzzleFXComponent in muzzleFXComponents) {
            muzzleFXComponent.TriggerEffect();
        }

        GameObject laser = (GameObject)Instantiate(echoStandardPrefab);
        LineRenderer laserRender = laser.GetComponent<LineRenderer>();
        Vector2 initialPos = (Vector2)transform.position;
        Vector2 targetPos = (Vector2)Format.mousePosInWorld();
        Vector2 laserDirection = (targetPos - initialPos).normalized;

        RaycastHit2D wallHit = Physics2D.Raycast(initialPos, laserDirection, float.MaxValue, wall);
        Assert.IsTrue(wallHit.collider.CompareTag("Wall"));

        laserRender.SetPosition(0, muzzlePosition.transform.position);
        laserRender.SetPosition(1, wallHit.point);

        GetComponentInParent<PlayerSounds>().playWallImpact();
        SimplePool.Spawn(muzzleSoundRenderingPrefab, wallHit.point);

        float maxLaserDistance = wallHit.distance;

        RaycastHit2D[] hitList = Physics2D.RaycastAll(initialPos, laserDirection, maxLaserDistance);
        foreach (RaycastHit2D hit in hitList) {
            if (hit.collider.CompareTag("Wall")) {
                GetComponentInParent<PlayerSounds>().playWallImpact();
                SimplePool.Spawn(muzzleSoundRenderingPrefab, hit.point);
            } else if (hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("Destructible")) {
                //lower Enemy health or Destructible object health
                hit.transform.GetComponent<Health>().Damage(damage);
                GetComponentInParent<PlayerSounds>().playFleshImpact();
                SimplePool.Spawn(muzzleSoundRenderingPrefab, hit.point);
            }
        }
        
        Camera.main.GetComponent<CameraShakeScript>().screenShake(0.2f, 0.1f);
    }
}
