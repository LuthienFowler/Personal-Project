using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
      /////////////////
     /// Variables ///
    /////////////////

    private float speed = 0.2f;
    private Rigidbody playerRb;

      /////////////////
     /// Functions ///
    /////////////////

    // Start is called before the first frame update
    void Start()
    {
        // Getting the player's rigid body
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Getting the input for the horizontal axis and vertical axis
        float hi = Input.GetAxis("Horizontal");
        float vi = Input.GetAxis("Vertical");

        // Simple movement
        transform.position += Vector3.forward * speed * vi;
        transform.position += Vector3.right * speed * hi;
        
    }



    // Controls

    // Use mouse to turn
    // wasd or arrows to move (But tbh does anyone really use the arrows?)
    // Space to jump
}
