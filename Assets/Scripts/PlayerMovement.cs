using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   
    public float speed = 4.0f;
  
    public float gravity = -9.8f;

    private CharacterController _charCont;
    //private Rigidbody rb;

    private float verticalVelocity = -9.8f;
    void Start()
    {
        _charCont = GetComponent<CharacterController>();
        //   rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = Mathf.Lerp(speed, 4.0f, 0.1f);
/*
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = Mathf.Lerp(speed, 12.0f,0.1f);
        }
        else
        {
            speed = Mathf.Lerp(speed, 4.0f, 0.1f);
        }


       
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _charCont.height = 0.2f;
            speed = 2.0f;
        }
        else
        {
            _charCont.height = 2.0f;
        }

        if (_charCont.isGrounded)
        {
            verticalVelocity = gravity * Time.deltaTime * 2;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = 5.0f;
            }
        }
        else
        {
            verticalVelocity += gravity*Time.deltaTime *2;
        }
        */

        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);


        movement.y = verticalVelocity;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _charCont.Move(movement);

        // just for player hitting objects with physics
        //  rb.velocity = movement;

    }
}