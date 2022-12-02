using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    public Camera cam;
    Vector3 offset;
    float initialPos;

    public float speed;
    public float horizontalInput;
    public float forwardInput;

    public float topSpeed = 200.0f;
    public float translatedTopSpeed = 4.0f;
    public float time = 5.0f;
    public int computedTime = 0;
    public int maxComputedTime = 2000;
    private bool isAccelerating = false;

    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();
        offset = cam.transform.position - transform.position;
        speed = 0;

        initialPos = transform.position.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameManager.IsGameActive && !gameManager.IsGameOver)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            forwardInput = Input.GetAxis("Vertical");

            float computedTimeF = (float)computedTime / 100;

            if (forwardInput == 0)
            {
                isAccelerating = false;
            }
            else
            {
                if (!isAccelerating)
                {
                    InvokeRepeating("ComputedTimeRoutine", 0, Time.fixedDeltaTime);
                    isAccelerating = true;
                }
                speed = computedTimeF / 1000 * topSpeed;
            }

            if (computedTime > maxComputedTime)
            {
                computedTime = maxComputedTime;
            }

            if (computedTime < 0)
            {
                computedTime = 0;
            }

            Vector3 targetPos = Vector3.right * speed;
            Vector3 targetPosLerp = Vector3.Lerp(transform.position, targetPos, time);

            transform.Translate(targetPosLerp);

            Vector3 sideDir = cam.transform.forward * -horizontalInput / 5;
            transform.Translate(sideDir);
            //rb.AddForce(sideDir, ForceMode.VelocityChange);

            gameManager.Score = (int) (transform.position.z - initialPos);
        }
    }

    void LateUpdate()
    {
        if (gameManager.IsGameActive && !gameManager.IsGameOver)
        {
            Vector3 movePosition = transform.position + offset;
            cam.transform.position = movePosition;
        }
    }

    void ComputedTimeRoutine()
    {
        if (isAccelerating)
        {
            if (forwardInput > 0)
            {
                computedTime++;
            }
            else
            {
                computedTime--;
            }
        }
    }

    public void Accelerate()
    {
        forwardInput = 1;
    }

    public void Break()
    {
        forwardInput = -1;
    }

    public void Right()
    {
        horizontalInput = 1;
    }

    public void Left()
    {
        horizontalInput = -1;
    }
}
