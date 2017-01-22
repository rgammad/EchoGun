using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTriggerBehavior : MonoBehaviour
{

    private float startTime;
    public float StartTime { set { this.startTime = value;} }

    private float growthRate;
    public float GrowthRate { set { this.growthRate = value; } }

    private float maxRadius;
    public float MaxRadius { set { this.maxRadius = value; } }

    private CircleCollider2D soundTrigger;
    void Start()
    {
        soundTrigger = gameObject.GetComponent<CircleCollider2D>();
        soundTrigger.radius = 0;
    }
    void Update()
    {
        if (soundTrigger.radius < maxRadius)
            soundTrigger.radius = growthRate * (Time.time - startTime);
        else
            Destroy(gameObject);
    }
}
