using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   [SerializeField] private float _speed;
    private PlayerMotor _motor;

    [SerializeField] private float _mouseSentitivityX = 3f;
    [SerializeField] private float _mouseSentitivityY = 3f;

    private void Start()
    {
        _motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        float xMov = Input.GetAxis("Horizontal");
        float zMov = Input.GetAxis("Vertical");

        Vector3 moveHozirontal = transform.right * xMov;
        Vector3 moveVertical = transform.forward * zMov;

        Vector3 _velocity = (moveHozirontal + moveVertical) * _speed;

     

        _motor.Move(_velocity);
        float yRot = Input.GetAxisRaw("Mouse X");
        Vector3 rotation = new Vector3(0, yRot, 0) * _mouseSentitivityX;

        _motor.Rotate(rotation);


        float xRot = Input.GetAxisRaw("Mouse Y");

        float cameraRotationX = xRot * _mouseSentitivityY;

        _motor.RotateCamera(cameraRotationX);

    }
}
