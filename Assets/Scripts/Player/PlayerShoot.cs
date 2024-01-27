using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Camera playerCamera;
    public float fireRate = 0.25f;
    public float weaponRange = 50f;
    public Transform gunEnd;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private float nextFire;

    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();

        }
    }

    void Shoot()
    {
        Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hit, weaponRange))
        {
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * 100f);
                print("PAN");

            }
        }
    }
}