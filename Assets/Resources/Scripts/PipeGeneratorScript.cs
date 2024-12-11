using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGeneratorScript : MonoBehaviour
{
    public GameObject pipePrefab; // El prefab de la tubería a instanciar
    public float minHeight = -1f; // Altura mínima donde puede aparecer la tubería
    public float maxHeight = 2.85f;  // Altura máxima donde puede aparecer la tubería
    public float spawnInterval = 2f; // Intervalo en segundos para generar las tuberías

    // Start is called before the first frame update
    void Start()
    {
        // Llama a la función que instanciará las tuberías cada 'spawnInterval' segundos
        InvokeRepeating("SpawnPipe", 0f, spawnInterval);
    }

    // Función para instanciar el prefab en una posición aleatoria
    void SpawnPipe()
    {
        // Calcula una posición aleatoria en el eje Y dentro del rango definido
        float randomHeight = Random.Range(minHeight, maxHeight);

        // Instancia el prefab de la tubería en la posición deseada
        Vector3 spawnPosition = new Vector3(transform.position.x, randomHeight, 0); // Mantiene la posición en X y Z del generador
        Instantiate(pipePrefab, spawnPosition, Quaternion.identity);
    }
}
