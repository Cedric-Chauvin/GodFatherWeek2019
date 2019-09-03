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
    public bool keyboard = false;

    [Header("Movement related settings")]
    public float deadzone = 0.1f;
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
        // Player movement

        float inputHorizontal;
        float inputVertical;
        Vector3 aim;

        if (!keyboard) // TWO JOYSTICKS
        {
            inputHorizontal = Input.GetAxis("P" + playerNumber + "_Horizontal");
            inputVertical = Input.GetAxis("P" + playerNumber + "_Vertical");

            float inputRightHorizontal = Input.GetAxis("P" + playerNumber + "_Horizontal_Right");
            float inputRightVertical = Input.GetAxis("P" + playerNumber + "_Vertical_Right");

            aim = new Vector3(inputRightHorizontal, 0f, inputRightVertical);

            // Applying rotation
            if (aim.magnitude > deadzone)
                transform.rotation = Quaternion.LookRotation(aim);
        }
        else // KEYBOARD
        {
            // Player rotation towards mouse

            Vector3 cursorScreenPoint = Input.mousePosition;
            Vector3 playerScreenPoint = viewCamera.WorldToScreenPoint(transform.position);

            // Z axis removed as it's the distance to the camera
            playerScreenPoint = new Vector3(playerScreenPoint.x, playerScreenPoint.y, 0f);

            aim = (cursorScreenPoint - playerScreenPoint).normalized;

            // Screen XY is world XZ
            aim = new Vector3(aim.x, 0f, aim.y);

            // Player movement

            inputHorizontal = Input.GetAxis("KB_Horizontal");
            inputVertical = Input.GetAxis("KB_Vertical");

            // Applying rotation
            transform.rotation = Quaternion.LookRotation(aim);
        }

        // Applying movement
        rigidBody.velocity = new Vector3(inputVertical * moveSpeed, 0.0f, inputHorizontal * -moveSpeed);
    }

    private void OnDestroy()
    {
        _players.Remove(this);
    }
}
