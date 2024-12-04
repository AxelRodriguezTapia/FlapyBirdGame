using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoAnimadoScript : MonoBehaviour
{
    // Velocidad de desplazamiento
    public float velocidad = 2f;

    // Referencias a los dos fondos
    public GameObject fondo1;
    public GameObject fondo2;

    // Largo de los fondos (deben ser del mismo tamaño)
    private float largo;

    void Start()
    {
        // Calcular el largo del fondo usando el tamaño de su SpriteRenderer
        largo = fondo1.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // Mover ambos fondos hacia la izquierda
        fondo1.transform.Translate(Vector3.left * velocidad * Time.deltaTime);
        fondo2.transform.Translate(Vector3.left * velocidad * Time.deltaTime);

        // Reposicionar el fondo que salga de la pantalla
        if (fondo1.transform.position.x < -largo)
        {
            fondo1.transform.position = new Vector3(fondo2.transform.position.x + largo, fondo1.transform.position.y, fondo1.transform.position.z);
        }
        else if (fondo2.transform.position.x < -largo)
        {
            fondo2.transform.position = new Vector3(fondo1.transform.position.x + largo, fondo2.transform.position.y, fondo2.transform.position.z);
        }
    }
}
