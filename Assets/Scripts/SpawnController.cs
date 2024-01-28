using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnController : MonoBehaviour
{
    // make multiple serializable fields
    
    [SerializeField]
    private List<GameObject> _enemyPrefab;
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
        foreach (Spawnner spawnner in spawnners)
        {
            Instantiate(_enemyPrefab[Random.Range(0,_enemyPrefab.Count-1)], spawnner.transform.position, Quaternion.identity);
            
        }

    }

    public void Resurerection()
    {
        StartCoroutine(ResurectionRoutine());
      
    }
    IEnumerator ResurectionRoutine()
    {
        yield return new WaitForSeconds(3f);

        int randomSpawnner = Random.Range(0, spawnners.Count);
        Spawnner spawnner = spawnners[randomSpawnner];
        Instantiate(_enemyPrefab[Random.Range(0, _enemyPrefab.Count - 1)], spawnner.transform.position, Quaternion.identity);

    }

}
