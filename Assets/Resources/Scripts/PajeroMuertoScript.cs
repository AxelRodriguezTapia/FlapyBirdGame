using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PajeroMuertoScript : MonoBehaviour
{

    private GameObject pajero;
    // Start is called before the first frame update
    void Start()
    {
        // Buscar al objeto "Pajero" para ver si aún está vivo
        pajero = GameObject.FindGameObjectWithTag("Pajero");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destruir el objeto Pipes al colisionar con un objeto que tenga un Collider marcado como Trigger
        //Debug.Log("Ha pegao");
        if (collision.gameObject.CompareTag("Pajero"))
        {
            pajero.GetComponent<PajeroScript>().setDead(true);
            Animator animator = pajero.GetComponent<Animator>();
            animator.SetTrigger("Die");
            Debug.Log("Animacion de Muerto");
        }
    }
}
