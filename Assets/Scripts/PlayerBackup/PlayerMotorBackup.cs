using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotorBackup : MonoBehaviour
{

    [SerializeField] Camera _camera;
    private Vector3 velocity;
    private Vector3 rotation;
    private float _cameraRotationX = 0f;

    private float _currentCameraRotationX = 0f;

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
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
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










//{
//    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
//    public RotationAxes axes = RotationAxes.MouseXAndY;
//    public float sensitivityX = 15F;
//    public float sensitivityY = 15F;

//    public float minimumX = -360F;
//    public float maximumX = 360F;

//    public float minimumY = -60F;
//    public float maximumY = 60F;

//    float rotationY = 0F;

//    void Update()
//    {
//        if (axes == RotationAxes.MouseXAndY)
//        {
//            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

//            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
//            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

//            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
//        }
//        else if (axes == RotationAxes.MouseX)
//        {
//            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
//        }
//        else
//        {
//            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
//            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

//            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
//        }
//    }

//    void Start()
//    {
      
//    }



