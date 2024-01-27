using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit,
                Mathf.Infinity))
        {

            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                EnemyController  ennemy = hit.collider.gameObject.GetComponent<EnemyController>();
               
                ennemy.Reaction(ennemy.GetCurrentState());
                
                
                //Destroy();
            }
        }
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
