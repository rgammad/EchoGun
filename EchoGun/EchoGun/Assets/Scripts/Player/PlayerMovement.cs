using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    protected MultiplierFloatStat maxSpeed = 15f;
    public MultiplierFloatStat MaxSpeed { get { return maxSpeed; } }

    [SerializeField]
    protected MultiplierFloatStat accel = 5f;
    public MultiplierFloatStat Accel { get { return maxSpeed; } }

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

    public Vector2 normalizedMovementInput { get { return new Vector2(Input.GetAxisRaw(bindings.HorizontalMovementAxisName), Input.GetAxisRaw(bindings.VerticalMovementAxisName)).normalized; } }

    public Vector2 rawAimingInput { get { return Format.mousePosInWorld() - transform.position; } }

    private void Start() {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        rigid.velocity = Vector2.ClampMagnitude(Vector2.MoveTowards(rigid.velocity, maxSpeed * normalizedMovementInput, maxSpeed * accel * Time.deltaTime), maxSpeed);
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
