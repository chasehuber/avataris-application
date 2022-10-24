using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Lots of this code is pieced together from the internet, but it works
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float airMultiplier;

    [Header("Ground check")]
    public Collider groundCheck;
    bool grounded;

    // Keeping track of camera and player orientation
    public Transform orientation;

    // Player inputs for movement
    public float horizontalInput;
    public float verticalInput;

    Vector3 moveDirection;
    Rigidbody rb;

    private void Start()
    {
        // Grabbing rigidbody and freezing the rotation of it
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        // Updates player inputs and keeps the speed from going past the set value
        MyInput();
        SpeedControl();

        // Setting drag for grounded player
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        // Actually moving player
        MovePlayer();
    }

    private void MyInput()
    {
        // Getting input values
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        // Actual player movement
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // If the player isn't grounded, the move speed changes so they can't fly around
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        // Calculates a flat velocity
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // Limits the velocity if player is exceeding it
        if(flatVel.magnitude > moveSpeed)
        {
            // Actually limiting velocity
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void OnTriggerEnter(Collider groundCheck)
    {
        // Uses a box collider to check if player is grounded or not, if it's not inside an object the player is not grounded
        grounded = true;
    }

    private void OnTriggerExit(Collider groundCheck)
    {
        grounded = false;
    }
}
