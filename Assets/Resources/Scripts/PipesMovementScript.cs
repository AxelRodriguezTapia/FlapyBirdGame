using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesMovementScript : MonoBehaviour
{
    public float velocidad = 1f;

    void Update()
    {
        // Mueve las tuberías hacia la izquierda
        transform.Translate(Vector2.left * velocidad * Time.deltaTime);

        // Verifica si la tubería ha salido de la pantalla
        if (transform.position.x < -5) // Ajusta este valor según el límite de tu cámara
        {
            // Destruye la tubería si ha salido de la pantalla
            Destroy(gameObject);
        }
    }
}
