using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PajeroMuertoScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destruir el objeto Pipes al colisionar con un objeto que tenga un Collider marcado como Trigger
        Debug.Log("Ha pegao");
        if (collision.gameObject.CompareTag("Pajero"))
        {
            Destroy(collision.gameObject);
            Debug.Log("PajeroMuerto");
        }
    }
}
