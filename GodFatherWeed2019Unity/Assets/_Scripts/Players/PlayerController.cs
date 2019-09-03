using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rigidBody;
    Camera viewCamera;

    public float moveSpeed = 10.0f;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        viewCamera = Camera.main;
        //	Cursor.visible = false;
    }

    void Update()
    {
        // Player rotation towards mouse

        Vector3 cursorScreenPoint = Input.mousePosition;
        Vector3 playerScreenPoint = viewCamera.WorldToScreenPoint(transform.position);

        // Z axis removed as it's the distance to the camera
        playerScreenPoint = new Vector3(playerScreenPoint.x, playerScreenPoint.y, 0f);

        Vector3 aim = (cursorScreenPoint - playerScreenPoint).normalized;

        // Screen XY is world XZ
        aim = new Vector3(aim.x, 0f, aim.y);

        transform.rotation = Quaternion.LookRotation(aim);

        // Player movement
        float inputHorizontal = Input.GetAxisRaw("P1_Horizontal");
        float inputVertical = Input.GetAxisRaw("P1_Vertical");

        rigidBody.velocity = new Vector3(inputVertical * moveSpeed, 0.0f, inputHorizontal * -moveSpeed);
    }
}
