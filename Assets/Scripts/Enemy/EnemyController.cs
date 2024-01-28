using System.Collections.Generic;
using Enemy;
using UnityEngine;

public enum EnemyState { Green, Yellow, Red }

public class EnemyController : MonoBehaviour
{

    [SerializeField] ParticleSystem _dispoParticules;
    [SerializeField] ParticleSystem _midDispoParticules;
    [SerializeField] ParticleSystem _noDispoParticules;
    [SerializeField] ParticleSystem _tearsParticules;
    [SerializeField] ParticleSystem _echecHitParticules;
    [SerializeField] ParticleSystem _successHisParticules;
    [SerializeField] ParticleSystem _explosion;
    private EnemyState currentState = EnemyState.Green;

    private float stateTimer = 2f;
    private AiEnemyController aiEnemyController;
    private SpawnController spawnController;
    private float timer;
    private float couldown = 8f;


    [SerializeField] List<AudioClip> cringes;
    [SerializeField] List<AudioClip> anger;
    [SerializeField] AudioSource audioManager;

    bool isDead = false;

    private void Start()
    {
        Debug.Log("stateTimer" + stateTimer);

        timer = stateTimer;
        aiEnemyController = GetComponent<AiEnemyController>();
        GameObject gameController = GameObject.Find("GameController");
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioSource>();
        spawnController = gameController.GetComponent<SpawnController>();
    }

    public EnemyState GetCurrentState()
    {
        return currentState;
    }

    private void FixedUpdate()
    {

        timer -= Time.deltaTime;
        couldown -= Time.deltaTime;

        if (timer <= 0)
        {
            ChangeState();
            timer = stateTimer;
        }
        if (couldown <= 0)
        {
            aiEnemyController.changeState(Mood.Escaping);
            couldown = 8f;
        }



        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit, Mathf.Infinity))
        {

            if (hit.collider.gameObject.CompareTag("Player"))
            {

                //Destroy();
            }
        }
        if(isDead)
        {
            _dispoParticules.Play();
            _midDispoParticules.Stop();
            _noDispoParticules.Stop();
        }
    }

    void ChangeState()
    {

        switch (currentState)
        {
            case EnemyState.Green:
                currentState = EnemyState.Yellow;
                _dispoParticules.Stop();
                _midDispoParticules.Play();
                _noDispoParticules.Stop();
                break;
            case EnemyState.Yellow:
                currentState = EnemyState.Red;
                _dispoParticules.Stop();
                _midDispoParticules.Stop();
                _noDispoParticules.Play();
                break;
            case EnemyState.Red:
                currentState = EnemyState.Green;
                _dispoParticules.Play();
                _midDispoParticules.Stop();
                _noDispoParticules.Stop();
                break;
        }
    }

    public void Reaction(EnemyState state)
    {

        switch (state)
        {
            case EnemyState.Green:
                aiEnemyController.changeState(Mood.Dying);
                SuccessHit();
                _tearsParticules.Stop();

                break;
            case EnemyState.Yellow:
                aiEnemyController.changeState(Mood.Escaping);
                audioManager.PlayOneShot(cringes[Random.Range(0, cringes.Count - 1)]);
                _tearsParticules.Play();
                break;
            case EnemyState.Red:
                aiEnemyController.changeState(Mood.Chaising);
                _echecHitParticules.Play();
                _tearsParticules.Stop();
                audioManager.PlayOneShot(anger[Random.Range(0, anger.Count - 1)]);
                break;
        }
    }

    private void SuccessHit()
    {
        isDead = true;
        _successHisParticules.Play();
        aiEnemyController._agent.speed = 0;
        transform.LookAt(GameObject.Find("Player").GetComponentInChildren<Camera>().transform);
        Invoke("ExploseTarget", 3f);

    }

    private void ExploseTarget()
    {
        _explosion.Play();
        Invoke("Destroy", 0.5f);

    }
    void Destroy()
    {
        Destroy(gameObject);
        spawnController.Resurerection();

    }

    void OnTriggerEnter(Collider other)
    {

        Debug.Log("Player entered the trigger zone.");
    }

    void OnTriggerStay(Collider other)
    {
        // Logique à exécuter tant que le joueur reste dans la zone
    }

    void OnTriggerExit(Collider other)
    {

        Debug.Log("Player left the trigger zone.");
    }
}
