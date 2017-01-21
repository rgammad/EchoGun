using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    protected float maxSpeed = 15;

    [SerializeField]
    protected float accel = 5;

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

    Vector2 normalizedMovementInput { get { return new Vector2(Input.GetAxis(bindings.HorizontalMovementAxisName), Input.GetAxis(bindings.VerticalMovementAxisName)).normalized; } }

    private void Start() {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        rigid.velocity = Vector2.ClampMagnitude(Vector2.MoveTowards(rigid.velocity, maxSpeed * normalizedMovementInput, maxSpeed * accel * Time.fixedDeltaTime), maxSpeed);

        //rotation
        /*
        if (aimingInputDirection.sqrMagnitude == 0 && normalizedMovementInput.sqrMagnitude != 0) //aimingInput rotation is handled when the aiming input is set
            rotateTowards(normalizedMovementInput);

        for (int i = 0; i<postFixedUpdateDelegates.Count; i++)
        {
            postFixedUpdateDelegates[i]();
        }

        normalizedMovementInput = cachedNormalizedMovementInput; //undo any modifications made by reversal abilities
        */
	}
    /*
    void rotateTowards(Vector2 targetDirection) {
    if (_rotationEnabled) {
        rotating.rotation = Quaternion.Slerp(rotating.rotation, targetDirection.ToRotation(), rotationLerpValue); //it's in fixed update, and the direction property should be used instead of sampling the transform
        _rotationDirection = targetDirection;
    }
    */
}
