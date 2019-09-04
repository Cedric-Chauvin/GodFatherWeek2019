using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static List<PlayerController> _players = new List<PlayerController>();

    private Rigidbody rigidBody;
    private Camera viewCamera;
    public Animator animator;

    [field: Header("Number of the player, corresponds to the controller"), SerializeField]
    public int playerNumber { get; private set; } = 1;

    [Header("Input related settings")]
    public bool keyboard = false;

    [Header("Movement related settings")]
    public float deadzone = 0.1f;
    [Range(500f, 1500f)]
    public float moveSpeed = 500f;

    [Header("Item")]
    private ObjectBase itemInRange;
    private ObjectBase currentItem;
    [Range(1f, 20f)]
    public float cooldown = 10f;
    private float lastPickupTime;

    [Range(0.1f, 2f)]
    public float damageCooldown = 0.5f;
    private float lastDamageTime;

    public float health { get; private set; } = 100f;
    private bool dead = false;

    private void OnDestroy()
    {
        _players.Remove(this);
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        viewCamera = Camera.main;

        lastPickupTime -= cooldown;
        lastDamageTime -= damageCooldown;
    }

    private void Start()
    {
        _players.Add(this);
    }

    private void Update()
    {
        if (dead) return;

        MovementAndOrientation();

        Interactions();
    }

    private void MovementAndOrientation()
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

        // Applying movement;
        rigidBody.velocity = new Vector3(inputHorizontal * moveSpeed * Time.deltaTime, rigidBody.velocity.y, inputVertical * moveSpeed * Time.deltaTime);

        if (currentItem)
        {
            currentItem.transform.position = transform.position;
            currentItem.transform.rotation = transform.rotation;
        }
        
        animator.SetFloat("Speed", rigidBody.velocity.sqrMagnitude);
    }

    private void Interactions()
    {
        if (currentItem && Input.GetButtonDown("P" + playerNumber + "_Action_Axis"))
        {
            if (currentItem.Utilisation(transform.rotation.eulerAngles.y, this))
                currentItem = null;
        }
        else if (itemInRange && Time.time > (lastPickupTime + cooldown) && Input.GetAxis("P" + playerNumber + "_Action_Axis") == 1f)
        {
            lastPickupTime = Time.time;
            currentItem = itemInRange;
            animator.SetBool("InRange", true);
            animator.SetTrigger("UseRT");
        }
    }

    public ObjectBase GetItemInRange()
    {
        return itemInRange;
    }

    public void SetItemInRange(ObjectBase obj)
    {
        itemInRange = obj;
    }

    public void Damage(float dmg)
    {
        if (health > 0f && lastDamageTime < Time.time + damageCooldown)
        {
            health -= dmg;

            // Call damage animation here (TODO)

            if (health <= 0f) Die();
        }
        else
            Debug.Log("Dead already!");
    }

    public void Die()
    {
        dead = true;

        // Call death animation here and ragdoll at the end of it via animation event (TODO)
    }
}
