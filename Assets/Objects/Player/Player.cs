using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    private Brake turnState;

    [Header("Bump")]
    [SerializeField] private float speedNeededToBump;
    [SerializeField] private float bumpForce;
    [SerializeField] private float gravity;
    bool isBumped;

    [SerializeField] private float angleToBounce;


    [Header("GroundCheck")]
    [SerializeField] private float distToGround;
    [SerializeField] private float timeBeforeCheckGood;
    [SerializeField] private float timeBeforeCheckBad;
    private float currentTimeBeforeCheck;

    [SerializeField] private LayerMask ground;

    [Header("Jump")]
    [SerializeField] private float jumpForce;

    [Header("Visual")]
    [SerializeField] private Sprite forward;
    [SerializeField] private Sprite turn;
    private SpriteRenderer sr;

    [SerializeField] private ParticleSystem driftLeft;
    [SerializeField] private ParticleSystem driftRight;
    
    
    
    enum Brake
    {
        Zero,
        Left,
        Right,
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();   
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();  
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

            if (currentSpeed > stepSpeed)
            {
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
            if (moveInputH <= -0.2f)
            {
                transform.Rotate(new Vector3(0, -rotationValue * leftBrake, 0));
                turnState = Brake.Left;
            }
            else if (moveInputH >= 0.2f)
            {
                transform.Rotate(new Vector3(0, rotationValue * rightBrake, 0));
                turnState = Brake.Right;
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, 0));
                turnState = Brake.Zero;
            }
        }

        switch (turnState)
        {
            case Brake.Left:
                sr.sprite = turn;
                sr.flipX= false;
                break;
            case Brake.Zero:
                sr.sprite = forward;
                sr.flipX= false;
                break;
            case Brake.Right:
                sr.sprite = turn;
                sr.flipX= true;
                break;
            default:
                break;
        }

        
    }

    private void Update()
    {
        currentTimeBeforeCheck -= Time.deltaTime;

        if (currentSpeed < stepSpeed) 
        { 
            driftRight.Stop();
            driftLeft.Stop();
        }
        else
        {
            switch (turnState)
            {
                case Brake.Zero:
                    driftRight.Stop();
                    driftLeft.Stop();
                    break;
                case Brake.Left:
                    driftRight.Stop();
                    if(!driftLeft.isEmitting)driftLeft.Play();
                    break;
                case Brake.Right:
                    if(!driftRight.isEmitting)driftRight.Play();
                    driftLeft.Stop();
                    break;
                default:
                    break;
            }
 
        }
    }


    private void Bump(Vector3 dir)
    {
        var angle = Vector3.Angle(transform.forward, dir);

        if (angle > angleToBounce)
        {
            currentSpeed = 0;
            currentTimeBeforeCheck = timeBeforeCheckBad;
        }
        else currentTimeBeforeCheck = timeBeforeCheckGood;

            


        isBumped = true;


        Vector3 newVect = dir.normalized + Vector3.up;
        rb.AddForce(newVect * bumpForce, ForceMode.Impulse);
    }

    private void Jump()
    {
        isBumped = true;
        currentTimeBeforeCheck = timeBeforeCheckGood;

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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
        if (collision.transform.tag != "Wall" || currentSpeed < speedNeededToBump) return;


        foreach (var item in collision.contacts)
        {
            Bump(Vector3.Reflect(transform.forward, item.normal));
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Jumper")
        {
            Jump();
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround, ground);
    }


}
