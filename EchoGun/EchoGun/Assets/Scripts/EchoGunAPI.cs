using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoGunAPI : MonoBehaviour {

    public GameObject echoStandardPrefab;
    public GameObject echoProjectilePrefab;
    public int echoProjSpeed;

    public void echoStandard(Vector2 aimDir)
    {
        GameObject laser = (GameObject)Instantiate(echoStandardPrefab);
        LineRenderer laserRender = laser.GetComponent<LineRenderer>();
        Vector2 initialPos = transform.position;
        Vector2 targetPos = aimDir;
        Vector2 laserEndPos = targetPos.normalized * 100;

        RaycastHit2D[] hitList = Physics2D.RaycastAll(initialPos, targetPos);
        foreach(RaycastHit2D hit in hitList)
        {
            if (hit.collider.CompareTag("Wall"))
            {
                laserEndPos = hit.point;
                break;
            }
            if (hit.collider.CompareTag("Enemy"))
                break; //Lower enemy HP
            if (hit.collider.CompareTag("Destructible"))
                break;//lower object hp
        }

        laserRender.SetPosition(0, this.transform.position);
        laserRender.SetPosition(1, laserEndPos);
    }

    public void echoProjectile(Vector2 aimDir)
    {
        ProjectileController pc = echoProjectilePrefab.GetComponent<ProjectileController>();
        pc.projSpeed = echoProjSpeed;
        SimplePool.Spawn(echoProjectilePrefab, transform.position, aimDir.ToRotation());
    }

}
