using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;
    //public Vector3 rotation = new Vector3(16.954f, 268.535f, -1.378f);

    public bool IsMoving { set; get; }

    private void Start()
    {
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        //if (!IsMoving)
        //return;

        Vector3 newPosition = new Vector3(offset.x + target.position.x, transform.position.y, transform.position.z);
        transform.position = newPosition;

        Vector3 desiredPosition = new Vector3(offset.x + target.position.x, transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime);
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation), 0.2f);
    }
}
