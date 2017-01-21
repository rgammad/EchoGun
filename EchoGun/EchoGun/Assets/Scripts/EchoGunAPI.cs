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
    public int echoProjSpeed;
    public float echoProjDamage;
    public float echoStandardDamage;



    public void echoStandard(WeaponType type, Vector2 aimDir)
    {
        GameObject laser = (GameObject)Instantiate(echoStandardPrefab);
        LineRenderer laserRender = laser.GetComponent<LineRenderer>();
		Vector2 initialPos = transform.position;
        Vector2 targetPos = aimDir;
        Vector2 laserEndPos = targetPos.normalized * 100;

        RaycastHit2D[] hitList = Physics2D.RaycastAll(initialPos, targetPos);

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
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        //lower Enemy health
                        PlayerPing.CreatePing(hit.point, 10.0f);
                        hit.transform.GetComponent<Health>().Damage(echoStandardDamage);
                        laserEndPos = hit.point;
                        break;
                    }
                    if (hit.collider.CompareTag("Destructible"))
                    {
                        //lower destructible health
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
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        PlayerPing.CreatePing(hit.point, 1.0f);
                        hit.transform.root.GetComponent<Health>().Damage(echoStandardDamage);
                        laserEndPos = hit.point;
                        break;
                    }
                    if (hit.collider.CompareTag("Destructible"))
                    {
                        PlayerPing.CreatePing(hit.point, 10.0f);
                        hit.transform.root.GetComponent<Health>().Damage(echoStandardDamage);
                        laserEndPos = hit.point;
                        break;
                    }
                }
                break;
        }
        laserRender.SetPosition(0, this.transform.position);
        laserRender.SetPosition(1, laserEndPos);

    }

    public void echoProjectile(WeaponType type, Vector2 aimDir)
    {
        ProjectileController pc = echoProjectilePrefab.GetComponent<ProjectileController>();
        pc.projSpeed = echoProjSpeed;

        pc.weapType = type;

        SimplePool.Spawn(echoProjectilePrefab, transform.position, aimDir.ToRotation());
    }

}
