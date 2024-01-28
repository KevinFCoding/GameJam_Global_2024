using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public  float _speed;
    public  float _speedMax;
    [SerializeField] private float _currentSpeed;
    private PlayerMotor _motor;
    public Rigidbody rb;
    private float gravityScale = 3f;
    [SerializeField] private float _mouseSentitivityX = 3f;
    [SerializeField] private float _mouseSentitivityY = 3f;

    public Vector3 jump;
    private float groundedDelay = 0.2f; // Délai de 0.2 secondes
    private float lastJumpTime;
    public bool isGrounded = true;
    private float gravity = 9.81f;
    private bool canDoubleJump = true;
    private void Start()
    {
        _motor = GetComponent<PlayerMotor>();
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        jump = new Vector3(0.0f,2.0f, 0.0f);
    }

    private void Update()
    {
        float xMov = Input.GetAxis("Horizontal");
        float zMov = Input.GetAxis("Vertical");


        Vector3 moveHozirontal = transform.right * xMov;
        Vector3 moveVertical = transform.forward * zMov;

        Vector3 _velocity = (moveHozirontal + moveVertical) * _currentSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = _speedMax;
        }
        else
        {
            _currentSpeed = _speed;
        }
     

        _motor.Move(_velocity);
        float yRot = Input.GetAxisRaw("Mouse X");
        Vector3 rotation = new Vector3(0, yRot, 0) * _mouseSentitivityX;

        _motor.Rotate(rotation);


        float xRot = Input.GetAxisRaw("Mouse Y");

        float cameraRotationX = xRot * _mouseSentitivityY;

        _motor.RotateCamera(cameraRotationX);


        if ( Input.GetKeyDown(KeyCode.Space) )
        {
            if (isGrounded && (Time.time - lastJumpTime >= groundedDelay))
            {
                rb.AddForce(new Vector3(0.0f, 2.0f, 0.0f) * 7f, ForceMode.Impulse);
                isGrounded = false;
                canDoubleJump = true;
                lastJumpTime = Time.time;  
            }else if (canDoubleJump)
            {
                rb.AddForce(new Vector3(0.0f, 2.0f, 0.0f) * 7f, ForceMode.Impulse);
                isGrounded = false;
                canDoubleJump = false;
                lastJumpTime = Time.time;  
            }

        }

    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.down * gravity * rb.mass * gravityScale);

    }


    void OnCollisionStay(Collision hit)
    {
        if (Time.time - lastJumpTime < groundedDelay) return; 

            // Même logique que dans OnCollisionStay
        foreach (ContactPoint contact in hit.contacts)
        {
            //Debug.Log("Vector3.Angle(contact.normal");
            //Debug.Log(Vector3.Angle(contact.normal,Vector3.up));
            if (Vector3.Angle(contact.normal, Vector3.up) < 80)
            {
                isGrounded = true;
                canDoubleJump = true;

                break;
            }
        }
    }


    void OnCollisionEnter(Collision hit)
    {
        if (Time.time - lastJumpTime < groundedDelay) return; 


        foreach (ContactPoint contact in hit.contacts)
        {
            if (Vector3.Angle(contact.normal, Vector3.up) < 45)
            {
                isGrounded = true;
                canDoubleJump = true;

                break;
            }
        }
        //}
    }

}
