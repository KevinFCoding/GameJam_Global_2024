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

    [SerializeField] ParticleSystem _goPart;
    [SerializeField] ParticleSystem _goRain;
    [SerializeField] GameObject _goBandes;
    public bool isAlive = true;
    [SerializeField] PlayerController _playerController;

    public int _cringe = 0;

    [SerializeField] List<AudioClip> jokes;
    [SerializeField] List<AudioClip> shootSounds;
    [SerializeField] AudioSource audioManager;
    [SerializeField] AudioSource audioJokesManager;



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

    public void TakeSuccessHit()
    {
        _cringe = 0;
        _cringeSlider.UpdateSliderLifeBar();

    }

    public void GameOver()
    {

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _goUI.SetActive(true);
        _goPart.Play();
        Invoke("ShowBandesGO", 1.5f);
    }

    public void ShowBandesGO()
    {
        _goBandes.SetActive(true);
        _goPart.Stop();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeCring(-5);

        }
        if (_cringe >= 35f)
        {
            isAlive = false;
            _goRain.Play();
            _playerController._speed = 0;
            _playerController._speedMax = 0;
            Invoke("GameOver", 2f);
          //  GameOver();
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

        audioJokesManager.PlayOneShot(shootSounds[Random.Range(0, shootSounds.Count - 1)], .7f);
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
                TakeSuccessHit();
                PlayJoke();
                break;
        }
    }
    private void PlayJoke()
    {
        audioJokesManager.PlayOneShot(jokes[Random.Range(0, jokes.Count - 1)], 1);
        audioManager.volume = 0.4f;
        Invoke("ResetVolume", 3f);
    }

    private void ResetVolume ()
    {
        audioManager.volume = 1f;
    }
    
    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Enemy"))
        {
            TakeCring(5);

        }
        Debug.Log("Player entered the trigger zone.");
    }

}
