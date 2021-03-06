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

    //Power ups 
    private bool jumpPowerupActivated = false;
    private bool speedPowerupActivated = false;
    private float powerupSpeedMultiplier = 5.0f;
    private float powerupJumpMultiplier = 5.0f;

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

        Debug.Log("Speed: " + speedPowerupActivated + " Jump: " + jumpPowerupActivated);
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

    // When the player collides with a trigger (In this case, a power up)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup_Jump"))
        {
            jumpPowerupActivated = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupJumpTimer());

        } else if (other.CompareTag("Powerup_Speed"))
        {
            speedPowerupActivated = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupSpeedTimer());
        }
    }

    // Making the jump power up timer
    IEnumerator PowerupJumpTimer()
    {
        yield return new WaitForSeconds(5);
        jumpPowerupActivated = false;
    }

    // Making the speed power up timer
    IEnumerator PowerupSpeedTimer()
    {
        yield return new WaitForSeconds(5);
        speedPowerupActivated = false;
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
        if (!speedPowerupActivated)
        {
            controller.Move(move * speed * Time.deltaTime);

        }else if (speedPowerupActivated)
        {
            controller.Move(move * speed * powerupSpeedMultiplier * Time.deltaTime);
        }
    }

    // Making the player jump
    void Jump()
    {
        // Jumping if the player is on the ground
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            // Checking if the player has a power up or not
            if (!jumpPowerupActivated)
            {
                vel.y = Mathf.Sqrt(jumpH * -2f * g);

            } else if (jumpPowerupActivated)
            {
                vel.y = Mathf.Sqrt(jumpH * -2f * g * powerupJumpMultiplier);

            }
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
