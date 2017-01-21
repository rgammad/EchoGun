using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

[RequireComponent(typeof(AudioController))]
[RequireComponent(typeof(ParticleSystem))]
public class BoostAbility : MonoBehaviour {
    
    PlayerMovement movement;
    float currentSpeedMod = 1;
    float currentAccelMod = 1;
    float currentMassMod = 1;
    AudioController sfx;
    ParticleSystem vfx;
    Rigidbody2D rigid;
    PlayerPing ping;

    Coroutine activationRoutine = null;

    //ability hardcoded to keycode.space

    protected void OnActivate()
    {
        sfx.Play();
        //StartCoroutine(playFX());
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
    protected float accelNerfDecayDuration = 1f;
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
        decayDuration = Mathf.Max(speedDecayDuration, accelNerfDecayDuration);
    }

    protected void Start()
    {
        movement = GetComponentInParent<PlayerMovement>();
        Assert.IsNotNull(movement);
        //vfx.startSize = 2*transform.parent.GetComponentInChildren<CircleCollider2D>().radius;
        rigid = GetComponentInParent<Rigidbody2D>();
        Assert.IsNotNull(rigid);
        ping = GetComponentInParent<PlayerPing>();
        Assert.IsNotNull(ping);
    }

    void Update() {

        if(Input.GetKeyDown(KeyCode.Space)) {

            Vector2 boostDirection = movement.normalizedMovementInput;
            if(boostDirection.magnitude == 0) {
                //not currently moving, use aiming direction instead
                boostDirection = movement.rawAimingInput;
            }

            if (activationRoutine != null) { //if currently active
                Reset();
            }
            activationRoutine = StartCoroutine(Boost(boostDirection));
        }

    }

    IEnumerator Boost(Vector2 direction) {
        ping.Ping(transform.position);

        currentMassMod = massBuff;
        movement.Mass.AddModifier(currentMassMod);

        currentSpeedMod = speedMultiplier;
        movement.MaxSpeed.AddModifier(currentSpeedMod);

        currentAccelMod = baseAccelDebuff;
        movement.Accel.AddModifier(currentAccelMod);

        rigid.velocity = movement.MaxSpeed.Value * direction.normalized;

        float activeEndTime = Time.time + activeDuration;

        yield return new WaitForSeconds(activeDuration);

        float decayEndTime = activeEndTime + decayDuration;
        while (Time.time < decayEndTime) {

            float speedDecayLerpValue = (Time.time - activeEndTime) / speedDecayDuration;
            if (speedDecayLerpValue > 1) {
                speedDecayLerpValue = 1;
            }

            movement.MaxSpeed.RemoveModifier(currentSpeedMod);
            currentSpeedMod = Mathf.Lerp(speedMultiplier, 1, speedDecayLerpValue);
            movement.MaxSpeed.AddModifier(currentSpeedMod);

            movement.Mass.RemoveModifier(currentMassMod);
            currentMassMod = Mathf.Lerp(massBuff, 1, speedDecayLerpValue);
            movement.Mass.AddModifier(currentMassMod);
            

            float accelDecayLerpValue = (Time.time - activeEndTime) / accelNerfDecayDuration;
            if (accelDecayLerpValue > 1) {
                accelDecayLerpValue = 1;
            }

            movement.Accel.RemoveModifier(currentAccelMod);
            currentAccelMod = Mathf.Lerp(baseAccelDebuff, 1, accelDecayLerpValue);
            movement.Accel.AddModifier(currentAccelMod);

            yield return null;
        }

        Reset();
    }

    private void Reset() {
        movement.MaxSpeed.RemoveModifier(currentSpeedMod);
        movement.Mass.RemoveModifier(currentMassMod);
        movement.Accel.RemoveModifier(currentAccelMod);
    }

    /*
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