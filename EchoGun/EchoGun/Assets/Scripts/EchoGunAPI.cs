using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    WEAPON_STANDARD,
    WEAPON_EXPLOSION
}

public class EchoGunAPI : MonoBehaviour
{

    [SerializeField]
    protected GameObject echoStandardPrefab;
    [SerializeField]
    protected GameObject echoProjectilePrefab;
    [SerializeField]
    protected Vector2 gunOffset = new Vector2(0.0f, -1.1f);
    [SerializeField]
    protected float shotSpread = 10f;

    public int echoProjSpeed;
    public float echoProjDamage;
    public float echoStandardDamage;
    public float echoStandardSoundRange = 30.0f;
    public float echoProjSoundRange = 25.0f;



    public void echoStandard(WeaponType type)
    {
        GameObject laser = (GameObject)Instantiate(echoStandardPrefab);
        LineRenderer laserRender = laser.GetComponent<LineRenderer>();
		Vector2 initialPos = (Vector2)transform.position;
        Vector2 targetPos = (Vector2)Format.mousePosInWorld();
        Vector2 laserEndPos = (targetPos - initialPos).normalized * 100;

        RaycastHit2D[] hitList = Physics2D.RaycastAll(initialPos, laserEndPos);

        switch (type)
        {
            case (WeaponType.WEAPON_STANDARD):

                foreach (RaycastHit2D hit in hitList)
                {
                    if (hit.collider.CompareTag("Wall"))
                    {
                        PlayerPing.CreatePing(hit.point, echoStandardSoundRange);
                        laserEndPos = hit.point;
                        GetComponent<playerSounds>().playWallImpact();
                        break;
                    }
                    if (hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("Destructible"))
                    {
                        //lower Enemy health or Destructible object health
                        PlayerPing.CreatePing(hit.point, echoStandardSoundRange);
                        hit.transform.GetComponent<Health>().Damage(echoStandardDamage);
                        laserEndPos = hit.point;
                        GetComponent<playerSounds>().playFleshImpact();
                        break;
                    }
                }
                break;
            case (WeaponType.WEAPON_EXPLOSION):
                break;
        }
		laserRender.SetPosition(0, (Vector2)transform.TransformPoint(gunOffset));
        laserRender.SetPosition(1, laserEndPos);

    }

    public void echoProjectile(WeaponType type)
    {
        Vector2 initialPos = (Vector2)transform.position;
        Vector2 targetPos = (Vector2)Format.mousePosInWorld();

        ProjectileController pc = echoProjectilePrefab.GetComponent<ProjectileController>();
        pc.projSpeed = echoProjSpeed;
        pc.soundRange = echoProjSoundRange;
        pc.weapType = type;

        Quaternion targetRotation = (targetPos - initialPos).ToRotation();
        targetRotation = targetRotation * Quaternion.AngleAxis(shotSpread * ((2 * Random.value) - 1), Vector3.forward);

        SimplePool.Spawn(echoProjectilePrefab, (Vector2)transform.TransformPoint(gunOffset), targetRotation);
    }

}
