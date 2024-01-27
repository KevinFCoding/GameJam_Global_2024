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
    [SerializeField] private CringeBar cringeBar;

    [SerializeField] ShakyCame _shakyCame;

    
    
    void Start()
    {
        cringeBar = GameObject.Find("CanvasPlayer").GetComponent<CringeBar>();
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }
    private void Update()
    {
        
        
        if (cringeBar.GetCringe() >= 100f)
        {
            // gameover
        }
        else
        {
            cringeAccumulator += Time.deltaTime;

            if (cringeAccumulator >= 1f)
            {
                cringeAccumulator -= 1f;
                cringeBar.SetCringe(cringeBar.GetCringe() + 1f);

            }
        }
        // increase cringe by one per second

        if (timeSinceLastShot < shootDelay)
        {
            timeSinceLastShot += Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    private float cringeAccumulator = 0f; // Accumulateur pour gérer l'incrément de cringe





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
                EnemyState states = ennemy.GetCurrentState();
                ChangeCringe(states);
                ennemy.Reaction(states);  
            }
        }
        _shootEffect.Play();

        // Réinitialiser le temps depuis le dernier tir
        timeSinceLastShot = 0f;
    }

    void ChangeCringe(EnemyState states)
    {
        float cringe = cringeBar.GetCringe();
        switch (states)
        {
            case EnemyState.Red:
                cringe += 10f;
                cringeBar.SetCringe(cringe);
                break;
            case EnemyState.Yellow:
                cringe += 5f;
                cringeBar.SetCringe(cringe);
                break;
            case EnemyState.Green:
                cringe -= 5f;
                cringeBar.SetCringe(cringe);
                break;
        }
    }

}
