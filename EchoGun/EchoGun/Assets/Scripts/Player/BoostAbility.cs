using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

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
    protected float activeDuration = 0.5f;
    [SerializeField]
    protected float speedDecayDuration = 1f;
    [SerializeField]
    protected float baseAccelDebuff = 0.066f;
    [SerializeField]
    protected float massBuff = 3f;
    [SerializeField]
    protected float accelNerfDecayTime = 1f;
    [SerializeField]
    protected float FXDurationExtend = 0.5f;

    /// <summary>
    /// How long it takes for all effects to fully decay.
    /// </summary>
    float decayDuration;

    protected void Awake()
    {
        sfx = GetComponent<AudioController>();
        vfx = GetComponent<ParticleSystem>();
        decayDuration = Mathf.Max(speedDecayDuration, accelNerfDecayTime);
    }

    protected void Start()
    {
        movement = GetComponentInParent<PlayerMovement>();
        Assert.IsNotNull(movement);
        //vfx.startSize = 2*transform.parent.GetComponentInChildren<CircleCollider2D>().radius;
        rigid = GetComponentInParent<Rigidbody2D>();
        Assert.IsNotNull(rigid);
    }

    protected void onFire(Vector2 direction)
    {
        StartCoroutine(Boost(direction));
    }

    IEnumerator Boost(Vector2 direction)
    {
        currentSpeedMod = speedMultiplier;
        movement.MaxSpeed.AddModifier(currentSpeedMod);

        currentAccelMod = baseAccelDebuff;
        movement.Accel.AddModifier(currentAccelMod);

        currentMassMod = massBuff;
        movement.Mass.AddModifier(currentMassMod);

        rigid.velocity = movement.MaxSpeed * direction.normalized;

        float activeEndTime = Time.time + activeDuration;

        yield return new WaitForSeconds(activeDuration);

        float decayEndTime = activeEndTime + decayDuration;
        while (Time.time < decayEndTime) {

            float speedDecayLerpValue = (Time.time - activeEndTime) / speedDecayDuration;
            if(speedDecayLerpValue <= 1) {
                movement.MaxSpeed.RemoveModifier(currentSpeedMod);
                currentSpeedMod = Mathf.Lerp(speedMultiplier, 1, speedDecayLerpValue);
                movement.MaxSpeed.AddModifier(currentSpeedMod);

                movement.Mass.RemoveModifier(currentMassMod);
                currentMassMod = Mathf.Lerp(massBuff, 1, speedDecayLerpValue);
                movement.Mass.AddModifier(currentMassMod);
            }

            speedMod.value = Mathf.Lerp(speedMultiplier, 1, lerpValue);
            massMod.value = Mathf.Lerp(massBuff, 1, lerpValue);
            if (time > boostDecayTime) {
                action.maxSpeedTracker.removeModifier(speedMod);
                action.mass.removeModifier(massMod);
                speedMod = null;
                massMod = null;
                yield break;
            }

            yield return null;
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