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

    private void Health_onDamage(float amount, int playerID)
    {
        //Debug.Log("I got into on damage");
        GetComponent<playerSounds>().playDamage();
    }

    private void Health_onDeath() {
        GetComponent<playerSounds>().playDeath();
        health.Heal(9999);

        canvasFlash.Flash();
        this.transform.root.position = new Vector3(100, -50);
    }
}
