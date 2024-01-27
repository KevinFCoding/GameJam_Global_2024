using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>())
        {
            collision.rigidbody.AddForce(new Vector3(0, 100f, 0), ForceMode.Impulse);
        }
    }
}
