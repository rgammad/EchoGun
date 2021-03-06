﻿using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

[RequireComponent(typeof(ParticleSystem))]
public class BoostAbility : MonoBehaviour {

    [SerializeField]
    protected GameObject soundPrefab;

    PlayerMovement movement;
    float currentSpeedMod = 1;
    float currentAccelMod = 1;
    float currentMassMod = 1;
    PlayerSounds soundController;
    ParticleSystem vfx;
    Rigidbody2D rigid;

    Coroutine activationRoutine = null;

    //ability hardcoded to keycode.space

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
        vfx = GetComponent<ParticleSystem>();
        soundController = GetComponentInParent<PlayerSounds>();
        ParticleSystem.MainModule main = vfx.main;
        decayDuration = Mathf.Max(speedDecayDuration, accelNerfDecayDuration);
    }

    protected void Start()
    {
        movement = GetComponentInParent<PlayerMovement>();
        Assert.IsNotNull(movement);
        //vfx.startSize = 2*transform.parent.GetComponentInChildren<CircleCollider2D>().radius;
        rigid = GetComponentInParent<Rigidbody2D>();
        Assert.IsNotNull(rigid);
    }

    void Update() {

        if(Input.GetKeyDown(KeyCode.Space)) {
            soundController.playDash();
            Vector2 boostDirection = movement.normalizedMovementInput;
            if(boostDirection.magnitude != 0) {
                //only boost if there is movement input

                if (activationRoutine != null) { //if not currently active
                    Reset();
                }

                vfx.Play();
                activationRoutine = StartCoroutine(Boost(boostDirection));
            }

            //create sound
            SimplePool.Spawn(soundPrefab, transform.position);
        }

    }

    IEnumerator Boost(Vector2 direction) {
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
        if(activationRoutine != null) {
            StopCoroutine(activationRoutine);
            activationRoutine = null;
        }
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