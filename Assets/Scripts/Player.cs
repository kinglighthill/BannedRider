using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Rigidbody rb;
    private Vector3 direction;
    //private Vector3 moveVector;
    //private Vector3 targetPosition;

    public float forwardSpeed;
    private float groundLevel;
    
    public int desiredLane = 4;
    public int newDesiredLane = 4;
    public bool? isGoingRight = null;

    private const float LANE_DISTANCE = 2.5f;

    private bool isRunning = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        //moveVector = Vector3.zero;
        //targetPosition = transform.position.x * Vector3.right;
    }

    //private void Update()
    //{
    //    direction.x = -forwardSpeed;

    //    if(Input.GetKeyDown(KeyCode.LeftArrow))
    //    //if (MobileInput.Instance.SwipeLeft)
    //    {
    //        isGoingRight = false;
    //        //MoveLane(false);
    //    }


    //    if (Input.GetKeyDown(KeyCode.RightArrow))
    //    //if (MobileInput.Instance.SwipeRight)
    //    {
    //        isGoingRight = true;
    //        //MoveLane(true);
    //    }

    //    Vector3 targetPosition = transform.position.x * Vector3.back;

    //    //if (isGoingRight.HasValue && )
    //    //{
    //    //    targetPosition += Vector3.forward * LANE_DISTANCE;
    //    //}
    //    //else
    //    //{
    //    //    targetPosition += Vector3.back * LANE_DISTANCE;
    //    //}

    //    switch (isGoingRight)
    //    {
    //        case true:
    //            targetPosition += Vector3.forward * LANE_DISTANCE;
    //            break;
    //        case false:
    //            targetPosition += Vector3.back * LANE_DISTANCE;
    //            break;
    //        default:
    //            break;
    //    }

    //    //desiredLane = newDesiredLane;

    //    //switch (desiredLane)
    //    //{
    //    //    case 0:
    //    //        targetPosition += Vector3.back * LANE_DISTANCE;
    //    //        break;
    //    //    case 2:
    //    //        targetPosition += Vector3.forward * LANE_DISTANCE;
    //    //        break;
    //    //    default:
    //    //        break;
    //    //}

    //    //transform.position = targetPosition;

    //    Vector3 moveVector = Vector3.zero;
    //    //moveVector.z = targetPosition.z;
    //    moveVector.z = (targetPosition - transform.position).normalized.z * forwardSpeed;


    //    moveVector.x = -forwardSpeed;

    //    controller.Move(moveVector * Time.deltaTime);

    //    //if (transform.position.y != groundLevel)
    //    //{
    //    //    transform.position.y = groundLevel;
    //    //}

    //    //controller.Move(direction * Time.fixedDeltaTime);

    //    //Vector3 dir = controller.velocity;
    //    //if (dir != Vector3.zero)
    //    //{
    //    //    dir.y = -0.3f;
    //    //    transform.right = Vector3.Lerp(transform.forward, dir, TURN_SPEED);
    //    //}
    //}

    private void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        rb.AddForce(Vector3.left * forwardSpeed * verticalInput * Time.deltaTime);
    }

    //private void FixedUpdate()
    //{
    //    //controller.Move(moveVector * Time.fixedDeltaTime);
    //    rb.AddForce(Vector3.left * forwardSpeed * Time.fixedDeltaTime);
    //}

    private void MoveLane(bool goingRight)
    {
        newDesiredLane = goingRight ? desiredLane + 1 : desiredLane - 1;
        newDesiredLane = Mathf.Clamp(newDesiredLane, 0, 8);

        if (newDesiredLane > desiredLane)
        {
            isGoingRight = true;
        }
        else if (newDesiredLane < desiredLane)
        {
            isGoingRight = false;
        }
        else
        {
            isGoingRight = null;
        }

        desiredLane = newDesiredLane;
    }
}
