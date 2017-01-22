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

    public GameObject echoStandardPrefab;
    public GameObject echoProjectilePrefab;
    public Vector2 gunOffset = new Vector2(0.0f, -1.1f);
    public int echoProjSpeed;
    public float echoProjDamage;
    public float echoStandardDamage;



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
                        PlayerPing.CreatePing(hit.point, 10.0f);
                        laserEndPos = hit.point;
                        break;
                    }
                    if (hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("Destructible"))
                    {
                        //lower Enemy health or Destructible object health
                        PlayerPing.CreatePing(hit.point, 10.0f);
                        hit.transform.GetComponent<Health>().Damage(echoStandardDamage);
                        laserEndPos = hit.point;
                        break;
                    }
                }
                break;
            case (WeaponType.WEAPON_EXPLOSION):
                break;
            default:
                foreach (RaycastHit2D hit in hitList)
                {
                    if (hit.collider.CompareTag("Wall"))
                    {
                        PlayerPing.CreatePing(hit.point, 10.0f);
                        laserEndPos = hit.point;
                        break;
                    }
                    if (hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("Destructible"))
                    {
                        //lower Enemy health or Destructible object health
                        PlayerPing.CreatePing(hit.point, 10.0f);
                        hit.transform.GetComponent<Health>().Damage(echoStandardDamage);
                        laserEndPos = hit.point;
                        break;
                    }
                }
                break;
        }
		laserRender.SetPosition(0, (Vector2)transform.TransformPoint(gunOffset));
        laserRender.SetPosition(1, laserEndPos);

    }

    public void echoProjectile(WeaponType type)
    {
        Vector2 initialPos = (Vector2)transform.position;
        Vector2 targetPos = (Vector2)Format.mousePosInWorld();
        Vector2 laserEndPos = (targetPos - initialPos).normalized * 100;

        ProjectileController pc = echoProjectilePrefab.GetComponent<ProjectileController>();
        pc.projSpeed = echoProjSpeed;

        pc.weapType = type;

        SimplePool.Spawn(echoProjectilePrefab, (Vector2)transform.TransformPoint(gunOffset), (targetPos - initialPos).ToRotation());
    }

}
