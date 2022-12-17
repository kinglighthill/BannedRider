using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public Camera cam;
    private CharacterController controller;

    private bool isIncoming = false;
    private float speed = 0.75f;

    public bool IsIncoming
    {
        get { return isIncoming; }
        set { isIncoming = value; }
    }

    //private Rigidbody rb;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        controller = GetComponent<CharacterController>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameManager.IsGameActive && !gameManager.IsGameOver && !gameManager.IsGamePaused)
        {
            float dir = isIncoming ? -1 : 1;
            Vector3 targetPos = Vector3.forward * speed * dir;
            Vector3 targetPosLerp = Vector3.Lerp(transform.position, targetPos, 5);

            controller.Move(targetPosLerp);

            float zPosition = transform.position.z;

            if (!isIncoming)
            {
                zPosition += 100;
            }

            if (cam.transform.position.z > zPosition)
            {
                Destroy(gameObject);
            }
        }
    }
}
