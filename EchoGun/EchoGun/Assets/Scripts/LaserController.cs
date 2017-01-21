using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour{
    public float lifetime = .25f;

    void Start()
    {
        Invoke("CleanUp", lifetime);
    }

    void CleanUp()
    {
        Destroy(this.gameObject);
    }
}

