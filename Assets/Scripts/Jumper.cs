using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{

    [SerializeField] float jumpingForce = 100f;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>())
        {
            collision.rigidbody.AddForce(new Vector3(0, jumpingForce, 0), ForceMode.Impulse);
        }
    }
}
