using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    public Camera cam;
    Vector3 offset;

    private int desiredLane = 1;


    public float speed = 700.0f;
    public float sideSpeed = 7f;
    public float speedIncreaseLastTick;
    public float speedIncreaseTime = 2.5f;
    public float speedIncreaseAmount = 0.1f;

    //private Animator anim;

    private const float LANE_DISTANCE = 2.5f;
    private const float TURN_SPEED = 0.05f;

    private bool isRunning = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        offset = cam.transform.position - transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 moveDir = cam.transform.forward * speed;
        float sideMove = Input.GetAxis("Horizontal");
        Vector3 sideDir = cam.transform.right * sideMove;
        Vector3 movement = moveDir + sideDir;
        rb.AddForce(moveDir);
        rb.AddForce(sideDir, ForceMode.VelocityChange);

        Debug.Log(sideMove);
        Debug.Log(sideDir);
    }

    void LateUpdate()
    {
        Vector3 movePosition = transform.position + offset;
        cam.transform.position = movePosition;
    }
}
