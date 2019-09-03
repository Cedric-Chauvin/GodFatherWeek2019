using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static List<PlayerController> _players = new List<PlayerController>();

    private Rigidbody rigidBody;
    private Camera viewCamera;

    [field: Header("Number of the player, corresponds to the controller"), SerializeField]
    public int playerNumber { get; private set; } = 1;

    [Header("Input related settings")]
    [Range(0.01f, 0.3f)]
    public float deadzone = 0.1f;
    public bool keyboard = false;

    [Header("Movement related settings")]
    [Range(1f, 20f)]
    public float moveSpeed = 10.0f;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        viewCamera = Camera.main;
    }

    private void Start()
    {
        _players.Add(this);
    }

    private void Update()
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

        float inputHorizontal = 0f;
        float inputVertical = 0f;

        if (!keyboard)
        {
            inputHorizontal = Input.GetAxis("P" + playerNumber + "_Horizontal");
            inputVertical = Input.GetAxis("P" + playerNumber + "_Vertical");

            inputHorizontal = Mathf.Abs(inputHorizontal) > deadzone ? inputHorizontal : 0f;
            inputVertical = Mathf.Abs(inputVertical) > deadzone ? inputVertical : 0f;
        }
        else
        {
            inputHorizontal = Input.GetAxis("KB_Horizontal");
            inputVertical = Input.GetAxis("KB_Vertical");
        }
        

        rigidBody.velocity = new Vector3(inputVertical * moveSpeed, 0.0f, inputHorizontal * -moveSpeed);
    }

    private void OnDestroy()
    {
        _players.Remove(this);
    }
}
