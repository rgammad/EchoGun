using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class WeaponSwapping : MonoBehaviour {

    [SerializeField]
    protected MonoBehaviour[] weapons;

    int currentlyActiveWeaponIndex;

	void Start () {
		if(weapons.Length == 0) {
            Debug.LogWarning("No weapons linked in the weaponSwapping script. Destroying self.");
            Destroy(this);
            return;
        }

        setCurrentlyActiveWeaponIndex(0);
    }

    void setCurrentlyActiveWeaponIndex(int index) {
        for(int i = 0; i < weapons.Length; i++) {
            //enabled if they are at the specified index, otherwise disabled
            weapons[i].enabled = (i == index);
        }
        currentlyActiveWeaponIndex = index;
    }
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Tab)) {
            int nextActiveIndex = (currentlyActiveWeaponIndex + 1) % weapons.Length;
            setCurrentlyActiveWeaponIndex(nextActiveIndex);
        }
	}
}
