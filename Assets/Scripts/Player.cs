using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private CharacterController controller;

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
        controller = GetComponent<CharacterController>();
        offset = cam.transform.position - transform.position;
        speed = 0;

        initialPos = transform.position.z;
    }

    void FixedUpdate()
    {
        if (gameManager.HasGameStarted)
        {
            //horizontalInput = Input.GetAxis("Horizontal");
            //forwardInput = Input.GetAxis("Vertical");

            float computedTimeF = (float) computedTime;

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
                speed = computedTimeF / 100000 * topSpeed;
            }

            if (computedTime > maxComputedTime)
            {
                computedTime = maxComputedTime;
            }

            if (computedTime < 0)
            {
                computedTime = 0;
            }

            Vector3 targetPos = Vector3.forward * speed;
            Vector3 targetPosLerp = Vector3.Lerp(transform.position, targetPos, time);

            Vector3 sideDir = Vector3.right * horizontalInput / 10;
            targetPosLerp += sideDir;

            controller.Move(targetPosLerp);

            gameManager.Score = (int) (transform.position.z - initialPos);
        }
    }

    void LateUpdate()
    {
        if (gameManager.HasGameStarted)
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
        horizontalInput = 0.05f;
    }

    public void Left()
    {
        horizontalInput = -0.05f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch (hit.gameObject.tag)
        {
            case "Vehicle":
                gameManager.HitVehicle();
                Vehicle vehicle = hit.gameObject.GetComponent<Vehicle>();

                if (!vehicle.IsIncoming)
                {
                    computedTime = Mathf.Min(20, computedTime);
                } else
                {
                    //gameManager.GameOver(false);
                }

                break;
        }
    }
}
