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
	}

    private void Health_onDeath() {
        health.Heal(9999);

        canvasFlash.Flash();
        this.transform.root.position = new Vector3(100, -50);
    }
}
