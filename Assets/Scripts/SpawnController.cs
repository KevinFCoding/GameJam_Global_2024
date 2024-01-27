using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnController : MonoBehaviour
{
    // make multiple serializable fields
    
    [SerializeField]
    private GameObject _enemyPrefab;
    public List<Spawnner> spawnners;
    void Start()
    {
        SpawnEnemy(spawnners);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy( List<Spawnner> spawnners)
    {
        // loop through the list of spawnners and spawn
        foreach (Spawnner spawnner in spawnners)
        {
            Instantiate(_enemyPrefab, new Vector3(spawnner.positionX, spawnner.positionY, spawnner.positionZ), Quaternion.identity);
            
        }

    }


}
