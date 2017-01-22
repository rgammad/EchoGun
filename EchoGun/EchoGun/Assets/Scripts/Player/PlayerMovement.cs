using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    protected float distanceTravelled = 0;

    [SerializeField]
    protected MultiplierFloatStat maxSpeed = new MultiplierFloatStat(15);
    public MultiplierFloatStat MaxSpeed { get { return maxSpeed; } }

    [SerializeField]
    protected MultiplierFloatStat accel = new MultiplierFloatStat(5);
    public MultiplierFloatStat Accel { get { return accel; } }

    [SerializeField]
    protected MultiplierFloatStat mass = new MultiplierFloatStat(1);
    public MultiplierFloatStat Mass { get { return mass; } }

    [Range(0, 1)]
    public float rotationSpeed = 0.2f;

    [System.Serializable]
    public class Bindings {
        [SerializeField]
        protected string horizontalMovementAxisName = "Horizontal";
        public string HorizontalMovementAxisName { get { return horizontalMovementAxisName; } }

        [SerializeField]
        protected string verticalMovementAxisName = "Vertical";
        public string VerticalMovementAxisName { get { return verticalMovementAxisName; } }
    }

    [SerializeField]
    protected Bindings bindings;

    Rigidbody2D rigid;

    Vector2 previousPos;

    public Vector2 normalizedMovementInput { get { return new Vector2(Input.GetAxisRaw(bindings.HorizontalMovementAxisName), Input.GetAxisRaw(bindings.VerticalMovementAxisName)).normalized; } }

    public Vector2 rawAimingInput { get { return Format.mousePosInWorld() - transform.position; } }


    private void Start() {
        rigid = GetComponent<Rigidbody2D>();
        mass.onValueChanged += Mass_onValueChanged;
        Mass_onValueChanged();
        previousPos = transform.position;
    }

    private void Mass_onValueChanged() {
        rigid.mass = mass;
    }

    void Update() {
        rigid.velocity = Vector2.ClampMagnitude(Vector2.MoveTowards(rigid.velocity, maxSpeed * normalizedMovementInput, maxSpeed * accel * Time.deltaTime), maxSpeed);

        distanceTravelled += (previousPos - (Vector2)transform.position).magnitude;
        previousPos = transform.position;

        while(distanceTravelled > 10.0f)
        {
            distanceTravelled -= 10.0f;
            //play sound
            GetComponent<playerSounds>().playFootstep();
        }
    }

    private void FixedUpdate() {
        rotateTowards(rawAimingInput);
	}

    void rotateTowards(Vector2 targetDirection) {
        //if (_rotationEnabled) {
        rigid.MoveRotation(Quaternion.Slerp(transform.rotation, targetDirection.ToRotation(), rotationSpeed).eulerAngles.z);
        //}
    }
}
