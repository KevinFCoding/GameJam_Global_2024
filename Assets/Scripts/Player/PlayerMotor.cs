using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{

    [SerializeField] Camera _camera;
    private Vector3 velocity;
    private Vector3 rotation;
    private float _cameraRotationX = 0f;

    private float _currentCameraRotationX = 0f;
    private bool grounded;
    [SerializeField]
    private float _cameraRotationLimit = 85f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }
    public void RotateCamera(float _cameraRotation)
    {
        _cameraRotationX = _cameraRotation;
    }
    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    public void PerformMovement()
    {
        if(velocity != Vector3.zero)
        {
            transform.position += velocity * Time.fixedDeltaTime;
            //rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    public void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        _currentCameraRotationX -= _cameraRotationX;
        _currentCameraRotationX = Mathf.Clamp(_currentCameraRotationX, -_cameraRotationLimit, _cameraRotationLimit);
        _camera.transform.localEulerAngles = new Vector3(_currentCameraRotationX, 0f, 0f);
    }



    
}


