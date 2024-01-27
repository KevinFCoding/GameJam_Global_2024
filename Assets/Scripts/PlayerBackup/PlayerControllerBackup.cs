using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerBackup : MonoBehaviour
{
   [SerializeField] private float _speed;
   [SerializeField] private float _speedMax;
    [SerializeField] private float _currentSpeed;
    private PlayerMotor _motor;
    public Rigidbody rb;
    public float gravityScale = 50;
    [SerializeField] private float _mouseSentitivityX = 3f;
    [SerializeField] private float _mouseSentitivityY = 3f;

    public Vector3 jump;
    public float jumpForce = 5.0f;

    public bool isGrounded = true;


    private float jumpHeight = 1.0f;
    private float gravityValue = 0f;
    private void Start()
    {
        _motor = GetComponent<PlayerMotor>();
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        jump = new Vector3(0.0f,20.0f, 0.0f);
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


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(jump, ForceMode.Impulse);
            isGrounded = false;
        }

        //if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        //{
        //    print("CONNARD");

        //    _velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        //}

    }

    private void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }
}
