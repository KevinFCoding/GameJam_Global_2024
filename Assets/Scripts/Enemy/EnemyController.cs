using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public enum EnemyState { Green, Yellow, Red }

public class EnemyController : MonoBehaviour
{
    
    
    private EnemyState currentState = EnemyState.Green;
    
    private float stateTimer = 4f;
    private AiEnemyController aiEnemyController;
    private float timer;

    private void Start()
    {
        Debug.Log("stateTimer"+stateTimer);

        timer = stateTimer;
        aiEnemyController = GetComponent<AiEnemyController>();

    }
    
    public EnemyState GetCurrentState()
    {
        return currentState;
    }

    private void FixedUpdate()
    {

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            ChangeState();
            timer = stateTimer;
        }
        
        
        
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("Did Hit");
                
                //Destroy();
            }
        }
        
    }

    void ChangeState()
    {
        switch (currentState)
        {

            
            case EnemyState.Green:
                currentState = EnemyState.Yellow;
                break;
            case EnemyState.Yellow:
                currentState = EnemyState.Red;
                break;
            case EnemyState.Red:
                currentState = EnemyState.Green;
                break;
        }
    }

    public void Reaction(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Green:
                Destroy();
                break;
            case EnemyState.Yellow:
                aiEnemyController.changeState(Mood.Escaping);
                break;
            case EnemyState.Red:
                aiEnemyController.changeState(Mood.Chaising);
                break;
        }
    }


    void Destroy()
    {
        Destroy(gameObject);
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
