using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Health))]
public class PlayerDeath : MonoBehaviour {

    [SerializeField]
    protected CanvasFlash canvasFlash;

    Health health;

	void Start () {
        health = GetComponent<Health>();
        health.onDeath += Health_onDeath;
        health.onDamage += Health_onDamage;
	}

    private void Health_onDamage(float amount)
    {
        //Debug.Log("I got into on damage");
        GetComponent<playerSounds>().playDamage();
    }

    private void Health_onDeath() {
        GetComponent<playerSounds>().playDeath();
        Camera.main.GetComponent<CameraShakeScript>().screenShake(3.0f, 5.0f);
        health.Heal(9999);

        canvasFlash.Flash();
        this.transform.root.position = new Vector3(100, -50);
    }
}
