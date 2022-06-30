using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
      /////////////////
     /// Variables ///
    /////////////////

    // movement vars
    public float speed = 12f;
    public CharacterController controller;
    public float jumpH = 3f;

    // Gravity
    Vector3 vel;
    public float g = -9.81f;

    // Ground check
    public Transform groundCheck;
    public float groundDist = 0.4f;
    public LayerMask groundMask;
    public bool isOnGround;

    // Boundries
    private float xBound = 24.5f;
    private float zBound = 24.5f;

      /////////////////
     /// Functions ///
    /////////////////

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckPhysics();
        Move();
        Jump();
        Fall();
        SetBounds();
    }

    // Checking if we are on the ground or not
    void CheckPhysics()
    {
        isOnGround = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);

        if (isOnGround && vel.y < 0)
        {
            vel.y = -2f;
        }
    }

    // Moving the player
    void Move()
    {
        // Getting the input for the horizontal axis and vertical axis
        float hi = Input.GetAxis("Horizontal");
        float vi = Input.GetAxis("Vertical");

        // Move variable
        Vector3 move = transform.right * hi + transform.forward * vi;

        // Simple movement
        controller.Move(move * speed * Time.deltaTime);
    }

    // Making the player jump
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            vel.y = Mathf.Sqrt(jumpH * -2f * g);
        }
    }

    // Making it so the player can fall
    void Fall()
    {
        // Increasing velocity over time
        vel.y += g * Time.deltaTime;

        // Having the player be able to fall
        controller.Move(vel * Time.deltaTime);
    }

    // Setting the boundaries of the game
    void SetBounds()
    {
        if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }

        if (transform.position.z > zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
        }
        else if (transform.position.z < -zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);
        }
    }
}
