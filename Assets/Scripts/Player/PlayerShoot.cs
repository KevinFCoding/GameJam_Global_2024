using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Camera playerCamera;
    public float fireRate = 0.25f;
    public float weaponRange = 50f;
    public Transform gunEnd;
    [SerializeField] ParticleSystem _shootEffect;
    //private WaitForSeconds shotDuration = new WaitForSeconds(2f);
    private float nextFire;
    private float timeSinceLastShot = 0f;
    private float shootDelay = 2f; // Délai de 5 secondes

    [SerializeField] ShakyCame _shakyCame;

    
    
    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }
    private void Update()
    {

        if (timeSinceLastShot < shootDelay)
        {
            timeSinceLastShot += Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }



    void Shoot()
    {
        if (timeSinceLastShot < shootDelay)
        {
            return; // Pas encore prêt à tirer
        }

        Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hit, weaponRange))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Did Hit");
                EnemyController ennemy = hit.collider.gameObject.GetComponent<EnemyController>();
                ennemy.Reaction(ennemy.GetCurrentState());  
            }
        }
        _shootEffect.Play();

        // Réinitialiser le temps depuis le dernier tir
        timeSinceLastShot = 0f;
    }

}
