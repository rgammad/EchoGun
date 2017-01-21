using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioController))]
[RequireComponent(typeof(ParticleSystem))]
public class BoostAbility : MonoBehaviour {
    /*
    PlayerMovement movement;
    float currentSpeedMod;
    float currentAccelMod;
    float currentMassMod;
    AudioController sfx;
    ParticleSystem vfx;
    Rigidbody2D rigid;

    protected void OnActivate()
    {
        sfx.Play();
        StartCoroutine(playFX());
    }

    [SerializeField]
    protected float speedMultiplier = 2.5f;
    [SerializeField]
    protected float maxDuration = 0.5f;
    [SerializeField]
    protected float boostDecayTime = 1f;
    [SerializeField]
    protected float baseAccelDebuff = 0.066f;
    [SerializeField]
    protected float massBuff = 3f;
    [SerializeField]
    protected float accelNerfDecayTime = 1f;
    [SerializeField]
    protected float FXDurationExtend = 0.5f;

    protected void Awake()
    {
        sfx = GetComponent<AudioController>();
        vfx = GetComponent<ParticleSystem>();
    }

    protected void Start()
    {
        movement = GetComponentInParent<PlayerMovement>();
        //vfx.startSize = 2*transform.parent.GetComponentInChildren<CircleCollider2D>().radius;
        rigid = GetComponentInParent<Rigidbody2D>();
    }

    protected void onFire(Vector2 direction)
    {
        StartCoroutine(Boost(direction));
    }

    IEnumerator Boost(Vector2 direction)
    {
        if (currentSpeedMod > 1)
            
        else
            speedMod.value = speedMultiplier;
        currentSpeedMod = movement.MaxSpeed.AddModifier(speedMultiplier);

        if (accelMod == null)
            accelMod = movement.accel.addModifier(baseAccelDebuff);
        else
            accelMod.value = baseAccelDebuff;

        if (massMod == null)
            massMod = movement.mass.addModifier(massBuff);
        else
            massMod.value = massBuff;

        rigid.velocity = movement.maxSpeedTracker * direction.normalized;

        active = true;
        float duration = 0;
        while (active)
        {
            yield return new WaitForFixedUpdate();
            duration += Time.fixedDeltaTime;
            if (duration > maxDuration)
            {
                active = false;
            }
        }

        StartCoroutine(DecaySpeed());
        StartCoroutine(DecayAccel());
    }
    IEnumerator DecaySpeed()
    {
        float time = 0;
        while (!active)
        {
            time += Time.fixedDeltaTime;
            float lerpValue = time / boostDecayTime;
            speedMod.value = Mathf.Lerp(speedMultiplier, 1, lerpValue);
            massMod.value = Mathf.Lerp(massBuff, 1, lerpValue);
            if (time > boostDecayTime)
            {
                action.maxSpeedTracker.removeModifier(speedMod);
                action.mass.removeModifier(massMod);
                speedMod = null;
                massMod = null;
                yield break;
            }

            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator DecayAccel()
    {
        float time = 0;
        while (!active)
        {
            time += Time.fixedDeltaTime;
            accelMod.value = Mathf.Lerp(baseAccelDebuff, 1, time / accelNerfDecayTime);
            if (time > boostDecayTime)
            {
                action.accel.removeModifier(accelMod);
                accelMod = null;
                yield break;
            }

            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator playFX()
    {
        vfx.Play();
        while (active)
            yield return null;
        Callback.FireAndForget(() => vfx.Stop(), FXDurationExtend, this);
    }

    protected override void Reset(float timeTillActive)
    {
        vfx.Stop();
    }
    */
}