using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PajeroScript : MonoBehaviour
{
    // La fuerza que se aplicará al objeto
    public float forceAmount = 0.2f;

    // Componente Rigidbody2D del objeto
    private Rigidbody2D rb;

    void Start()
    {
        // Obtener el Rigidbody2D del objeto al que está adjunto el script
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Comprobar si el jugador toca la pantalla (o hace clic)
        if (Input.GetMouseButtonDown(0)) // 0 significa clic izquierdo o primer toque
        {
            // Verificar que el objeto tiene un Rigidbody2D
            if (rb != null)
            {
                // Poner la velocidad a 0 (detener el objeto antes de aplicar la fuerza)
                rb.velocity = Vector2.zero;

                // Calcular la dirección hacia la que se aplicará la fuerza
                // En este caso, vamos a aplicar la fuerza hacia arriba en el eje Y
                Vector2 forceDirection = Vector2.up; // Puedes cambiar esta dirección si quieres aplicar la fuerza en otra dirección

                // Aplicar la fuerza al objeto
                rb.AddForce(forceDirection * forceAmount, ForceMode2D.Impulse);
            }
        }
    }
}
