using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;

    private int desiredLane = 1;


    public float originalSpeed = 7.0f;
    public float speed;
    public float speedIncreaseLastTick;
    public float speedIncreaseTime = 2.5f;
    public float speedIncreaseAmount = 0.1f;

    //private Animator anim;

    private const float LANE_DISTANCE = 2.5f;
    private const float TURN_SPEED = 0.05f;

    private bool isRunning = false;

    void Start()
    {
        speed = originalSpeed;
        controller = GetComponent<CharacterController>();
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Time.time - speedIncreaseLastTick > speedIncreaseTime)
        //{
        //    speedIncreaseLastTick = Time.time;
        //    speed += speedIncreaseAmount;
        //    //GameManager.Instance.UpdateModifier(speed - originalSpeed);
        //}

        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //if (MobileInput.Instance.SwipeLeft)
            //MoveLane(false);


        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //if (MobileInput.Instance.SwipeRight)
            //MoveLane(true);

        Vector3 targetPosition = transform.position.x * Vector3.forward;

        //switch (desiredLane)
        //{
        //    case 0:
        //        targetPosition += Vector3.left * LANE_DISTANCE;
        //        break;
        //    case 2:
        //        targetPosition += Vector3.right * LANE_DISTANCE;
        //        break;
        //    default:
        //        break;
        //}

        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).normalized.x * speed;

        
        //moveVector.z = speed;

        controller.Move(moveVector * Time.deltaTime);

        //Vector3 dir = controller.velocity;
        //if (dir != Vector3.zero)
        //{
        //    dir.y = 0;
        //    //transform.forward = Vector3.Lerp(transform.forward, dir, TURN_SPEED);
        //}
    }
}
