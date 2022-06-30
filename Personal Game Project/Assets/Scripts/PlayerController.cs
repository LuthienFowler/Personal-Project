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
        // Getting the input for the horizontal axis and vertical axis
        float hi = Input.GetAxis("Horizontal");
        float vi = Input.GetAxis("Vertical");

        // Move variable
        Vector3 move = transform.right * hi + transform.forward * vi;


        // Simple movement
        controller.Move(move * speed * Time.deltaTime);

    }

    // Controls

    // Use mouse to turn
    // wasd or arrows to move (But tbh does anyone really use the arrows?)
    // Space to jump
}
