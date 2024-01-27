using UnityEngine;

// Ce script est le SpawnerMethods,
// celui qui contient la methode de spawn qui peut etre appele depuis tous les scripts
// du moment qu'il a la reference du scipt

// Ce script est a placer sur un game object a part "spawnerManager"

// Il faut donner un gameObject et un transfrom a cette methode 
public class SpawnerMethods : MonoBehaviour
{
    public void Spawn(GameObject objectToSpawn, Transform transform)
    {
        if (objectToSpawn == null)
        {
            return;
        }
        else
        {
            Instantiate(objectToSpawn, transform);
        }
    }
}
