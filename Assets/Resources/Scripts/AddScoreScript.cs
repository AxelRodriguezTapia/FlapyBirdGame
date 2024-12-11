using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Necesario para interactuar con la UI
using TMPro;
using UnityEngine.SceneManagement;  // Importa SceneManager

public class AddScoreScript : MonoBehaviour
{
    public int score;  // La puntuación
    public TextMeshProUGUI scoreText;  // Referencia al componente Text en la UI
    public GameObject restartButton; // El botón de reinicio
    private GameObject pajero;

    void Start()
    {
        score = 0;
        UpdateScoreText();  // Muestra la puntuación inicial
        restartButton.SetActive(false); // Asegurarse de que el botón esté desactivado al inicio
    }

    void Update()
    {
        // Buscar al objeto "Pajero" para ver si aún está vivo
        pajero = GameObject.FindGameObjectWithTag("Pajero");

        // Si el objeto "Pajero" ya no está presente, activar el botón de reinicio
        if (pajero == null)
        {
            restartButton.SetActive(true); // Activar el botón cuando el pájaro muera
        }
    }

    // Método que se llama para sumar puntos
    public void sumScore()
    {
        score++;
        Debug.Log(score);
        UpdateScoreText();  // Actualiza el texto de la puntuación
    }

    // Actualiza el texto de la puntuación en la UI
    void UpdateScoreText()
    {
        scoreText.text = "Puntuació: " + score.ToString();
    }

    // Método para reiniciar la escena
    public void RestartLevel()
    {
        // Obtener el nombre de la escena actual y recargarla
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
