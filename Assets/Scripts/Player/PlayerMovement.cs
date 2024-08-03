using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Game object references ============================================
    PlayerInput playerInput;

    public Rigidbody rb;
    public Camera fpsCamera;
    public Transform player;

    public Transform groundCheck;
    float checkSize = 0.1f;

    public InventoryObject inventory;

    // Player Stats =======================================================
    private float speed;
    public float walkSpeed;
    public float runSpeed;
    public float crouchSpeed;
    public float jumpForce;

    public float pickUpRange;

    public float Gravity;
    public int grav = 0;

    public float airSpeed;
    public float maxVelocity;

    // Player States =====================================================
    public bool isCrouched;
    public bool isGrounded;
    bool canJump;
    public bool isSprinting;

    public bool isItem;


    // Layer Masks ======================================================
    public LayerMask groundMask;
    public LayerMask itemMask;

    // Start is called before the first frame update
    void Start()
    {

        playerInput = GetComponent<PlayerInput>();
        speed = walkSpeed;

    }

    // Update is called once per frame
    void Update()
    {

        // Camera always sticks to player head
        fpsCamera.transform.position = player.position + new Vector3(0, 1, 0); 

        // Spawns sphere at player feet and checks for ground collisions
        isGrounded = Physics.CheckSphere(groundCheck.transform.position, checkSize, groundMask);

        if(Input.GetKeyDown(playerInput.jumpKey) && isGrounded)
        {
            canJump = true;
        }

        if (Input.GetKeyDown(playerInput.pickUp))
        {

            Ray();

            //RaycastHit check;
            //isWallRight = Physics.Raycast(transform.position, player.right, out check, 2f, wall);

        }


    }

    void FixedUpdate()
    {

        //Jump Code ================================================================================

        if (canJump == true)
        {
            rb.AddForce(Vector3.up * jumpForce);
            canJump = false;
        }

        //Smooth movement in fixed update =========================================================

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical) * speed * Time.deltaTime;

        if (isGrounded)
        {
            transform.Translate(moveDirection);
        }
        else
        {

            // Add gravity once player is near peak of jump (higher number applies gravity earlier)
            if(rb.velocity.y < 4)
            {
                rb.AddForce(Vector3.up * -Gravity);
            }

            transform.Translate(moveDirection * airSpeed);
        }

        //Speed Cap ==============================================================================

        if (rb.velocity.sqrMagnitude > maxVelocity)
        {
            //smoothness of the slowdown is controlled by the 0.99f, 
            //0.5f is less smooth, 0.9999f is more smooth
            rb.velocity *= 0.7f;
        }

    }

    // Raycast Function ==========================================================================

    void Ray()
    {
        RaycastHit hit;

        // Debug Raycast, functionally useless
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, pickUpRange))
        {
            Debug.DrawRay(fpsCamera.transform.position, fpsCamera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue);
            Debug.Log(hit.transform.name);

        }

        isItem = Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, pickUpRange, itemMask);

        // Item pickup functionality
        if(isItem)
        {
            var item = hit.transform.gameObject.GetComponent<Item>();
            inventory.AddItem(item.item, 1);

            //Remove item once picked up
            Destroy(hit.transform.gameObject);
        }

    }

    private void OnApplicationQuit()
    {
        inventory.Backpack.Clear();
    }

}
