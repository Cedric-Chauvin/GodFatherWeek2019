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
    [Range(1f, 10f)]
    public float moveSpeed = 4f;

    [Header("Items")]
    private ObjectBase itemInRange;
    private ObjectBase currentItem;

    [Header("Cooldowns")]
    [Range(1f, 20f)]
    public float pickupCooldown = 4f;
    private float lastPickupTime;

    [Range(0.1f, 2f)]
    public float damageCooldown = 0.5f;
    private float lastDamageTime;

    [Range(0.5f, 2f)]
    public float beforeUseCooldown = 1f;

    [field: Header("Health")]
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
        animator = GetComponentInChildren<Animator>();

        lastPickupTime -= pickupCooldown;
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
        // Player movement and rotation

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

            // Applying rotation
            transform.rotation = Quaternion.LookRotation(aim);

            // Player movement
            inputHorizontal = Input.GetAxis("KB_Horizontal");
            inputVertical = Input.GetAxis("KB_Vertical");
        }

        // Applying movement;
        rigidBody.velocity = new Vector3(Mathf.Clamp(inputHorizontal, -1, 1) * moveSpeed, rigidBody.velocity.y, Mathf.Clamp(inputVertical, -1, 1) * moveSpeed);

        // Make current item follow player
        if (currentItem)
        {
            currentItem.transform.position = transform.position;
            currentItem.transform.rotation = transform.rotation;
        }
        
        animator.SetFloat("Speed", rigidBody.velocity.sqrMagnitude); // RUNNING ANIMATION
    }

    private void Interactions()
    {

        if (currentItem && Input.GetAxis("P" + playerNumber + "_Action_Axis") == 1f && Time.time > (lastPickupTime + beforeUseCooldown))
        {
            if (currentItem.Utilisation(transform.rotation.eulerAngles.y, this))
                currentItem = null;
        }
        else if (itemInRange && Time.time > (lastPickupTime + pickupCooldown) && Input.GetAxis("P" + playerNumber + "_Action_Axis") == 1f)
        {
            lastPickupTime = Time.time;
            currentItem = itemInRange;

            // Remove picked up item from in range, from all players
            foreach (PlayerController pc in _players)
                if (pc.GetItemInRange() == currentItem) pc.SetItemInRange(null);          

            // Remove pickup script from current item
            Destroy(currentItem.gameObject.GetComponent<ObjectPickUp>());

            animator.SetBool("InRange", true); // PICKUP ANIMATION
            animator.SetTrigger("UseRT"); // PICKUP ANIMATION

            animator.SetBool("InRange", false); // RESET MULTI CONDITION
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
        if (health > 0f && Time.time > (lastDamageTime + damageCooldown))
        {
            lastDamageTime = Time.time;
            health -= dmg;
            Debug.Log(health);

            animator.SetTrigger("Hit"); // HIT ANIMATION

            if (health <= 0f) Die();
        }
        else
            Debug.Log("Dead already!");
    }

    public void Die()
    {
        animator.SetBool("Alive", false); // Death animation
        dead = true;
    }
}
