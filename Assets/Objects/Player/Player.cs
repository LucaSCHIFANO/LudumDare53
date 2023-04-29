using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private float moveInputH;
    private bool isMovingForward;
    private bool isMovingBarward;
    private Rigidbody rb;

    [Header("Speed")]
    [SerializeField] private float stepSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float acceleration2;
    [SerializeField] private float deceleration;
    private float currentSpeed;

    [Header("Go Back")]
    [SerializeField] private float backSpeed;
    [SerializeField] private float maxBackSpeed;

    [Header("Rotation")]
    [SerializeField] private float rotationValue;
    [SerializeField] private float brakeRotationMultiplicator;
    private Brake brakeState;

    [Header("Bump")]
    [SerializeField] private float bumpForce;
    [SerializeField] private float gravity;
    bool isBumped;

    [Header("GroundCheck")]
    [SerializeField] private float distToGround;
    [SerializeField] private float timeBeforeCheck;
    private float currentTimeBeforeCheck;

    [SerializeField] private float angleToBounce;

    [SerializeField] private LayerMask ground;


    enum Brake
    {
        Zero,
        Left,
        Right,
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();   
    }

    private void FixedUpdate()
    {
        float leftBrake = 1;
        float rightBrake = 1;

        rb.AddRelativeForce(Vector3.down * gravity);

        if (isBumped)
        {
            if(IsGrounded() && currentTimeBeforeCheck <= 0 ) isBumped = false;
            else return;
        }



        if (isMovingForward)
        {
            if (currentSpeed < stepSpeed) currentSpeed += acceleration;
            else currentSpeed += acceleration2;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);


            switch (brakeState)
            {
                case Brake.Left:
                    leftBrake = brakeRotationMultiplicator;
                    break;
                case Brake.Right:
                    rightBrake = brakeRotationMultiplicator;
                    break;
                default:
                    break;
            }

        }
        else if (isMovingBarward)
        {
            currentSpeed -= backSpeed;
            currentSpeed = Mathf.Clamp(currentSpeed, maxBackSpeed, maxSpeed);

        }
        else
        {
            currentSpeed -= deceleration;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        }

        rb.velocity = (transform.forward).normalized * currentSpeed;


        if(Mathf.Abs(currentSpeed) > 2)
        {
            if (moveInputH <= -0.2f) transform.Rotate(new Vector3(0, -rotationValue * leftBrake, 0));
            else if (moveInputH >= 0.2f) transform.Rotate(new Vector3(0, rotationValue * rightBrake, 0));
            else transform.Rotate(new Vector3(0, 0, 0));
        }

    }

    private void Update()
    {
        currentTimeBeforeCheck -= Time.deltaTime;
    }
    private void Bump(Vector3 dir)
    {
        var angle = Vector3.Angle(transform.forward, dir);
        Debug.Log(angle);

        if (angle > angleToBounce)
        {
            currentSpeed = 0;
        }

            isBumped = true;
            currentTimeBeforeCheck = timeBeforeCheck;




        Vector3 newVect = dir.normalized + Vector3.up;
        rb.AddForce(newVect * bumpForce, ForceMode.Impulse);
    }

    // Input system --------------------------------------------------------------------------
    public void Movement(InputAction.CallbackContext context)
    {
        moveInputH = context.ReadValue<Vector2>().x;
        //moveInputV = context.ReadValue<Vector2>().y;
    }

    public void Forward(InputAction.CallbackContext context)
    {
        if (context.started) isMovingForward = true;
        if (context.canceled) isMovingForward = false;
    }

    public void Back(InputAction.CallbackContext context)
    {
        if (context.started) isMovingBarward= true;
        if (context.canceled) isMovingBarward = false;
    }

    public void BrakeLeft(InputAction.CallbackContext context)
    {
        if (context.started) brakeState = Brake.Left;
        if (context.canceled && brakeState == Brake.Left) brakeState = Brake.Zero;
    }

    public void BrakeRight(InputAction.CallbackContext context)
    {
        if (context.started) brakeState = Brake.Right;
        if (context.canceled && brakeState == Brake.Right) brakeState = Brake.Zero;
    }




    // Collision --------------------------------------------------------------------------

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Wall" || currentSpeed <= stepSpeed ) return;

        foreach (var item in collision.contacts)
        {
            Bump(Vector3.Reflect(transform.forward, item.normal));
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround, ground);
    }


}
