using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rigidBody;
    Camera viewCamera;
    Vector3 velocity;

    public float moveSpeed = 10.0f;
    // Use this for initialization

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        viewCamera = Camera.main;
        //	Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 mousePos = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, viewCamera.transform.position.y));
        //transform.LookAt(mousePos + Vector3.up * transform.position.y);
        float inputHorizontal = Input.GetAxisRaw("P1_Horizontal");
        float inputVertical = Input.GetAxisRaw("P1_Vertical");
        Vector3 newVelocity = new Vector3(inputVertical * moveSpeed, 0.0f, inputHorizontal * -moveSpeed);
        rigidBody.velocity = newVelocity;
    }
}
