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
    private float nextFire;
    private float timeSinceLastShot = 0f;
    private float shootDelay = 0.3f; // Délai de 5 secondes
    [SerializeField] private CringeBar cringeBar;

    [SerializeField] ShakyCame _shakyCame;
    [SerializeField] GameObject _goUI;
    [SerializeField] PauseMenu _pauseMenu;
    [SerializeField] CringeBar _cringeSlider;

    public int _cringe = 0; 



    void Start()
    {
        cringeBar = GameObject.Find("CanvasPlayer").GetComponent<CringeBar>();
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
        _cringeSlider.cringeSlider.value = _cringe;
        _cringeSlider.UpdateSliderLifeBar();
    }

    public void TakeCring(int _taken)
    {
        _cringe += _taken;
        _cringeSlider.UpdateSliderLifeBar();

    }

    public void GameOver()
    {
        _goUI.SetActive(true);
        _pauseMenu.Paused();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeCring(-5);

        }
        if (_cringe >= 35f)
        {
            GameOver();
        }
        else
        {
            cringeAccumulator += Time.deltaTime;

            if (cringeAccumulator >= 1f)
            {
                cringeAccumulator -= 1f;
                _cringe+=1;
                _cringeSlider.UpdateSliderLifeBar();
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
        
        switch (states)
        {
            case EnemyState.Red:
                TakeCring(5);

                break;
            case EnemyState.Yellow:
                TakeCring(1);

                break;
            case EnemyState.Green:
                TakeCring(-5);


                break;
        }
    }

}
