using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{
    protected Joystick joystick;

    float speed = 8;
    float rotSpeed = 80;
    float gravity = 8;
    float rot = 0f;

    Vector3 moveDir = Vector3.zero;

    CharacterController controller;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        joystick = FindObjectOfType<Joystick>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            // keyboard

            if (Input.GetKey(KeyCode.W))
            {
                anim.SetInteger("IsRunning", 1);
                moveDir = new Vector3(0, 0, 1);
                moveDir *= speed;
                moveDir = transform.TransformDirection(moveDir);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                anim.SetInteger("IsRunning", 0);
                moveDir = new Vector3(0,0,0);
            }


            // virtual joystick

            if (joystick.Vertical > 0.5 || joystick.Horizontal > 0.5 || joystick.Vertical < -0.5 || joystick.Horizontal < -0.5)
            {
                anim.SetInteger("IsRunning", 1);
                moveDir = new Vector3(0, 0, 1);
                moveDir = transform.TransformDirection(moveDir);
                //moveDir = cameraTransform.TransformDirection(moveDir);
                moveDir *= speed;

            } 
            else
            {
                    anim.SetInteger("IsRunning", 0);
                    moveDir = new Vector3(0, 0, 0);
            }




        }

        //rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;

        // probleem is dat de rotatie moet worden opgeslagen nadat de joystick kleinere waardes heeft dan 0.5
        if (joystick.Vertical > 0.5 || joystick.Horizontal > 0.5 || joystick.Vertical < -0.5 || joystick.Horizontal < -0.5)
        {
            transform.localEulerAngles = new Vector3(0, Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg, 0);
        }





        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }
}
