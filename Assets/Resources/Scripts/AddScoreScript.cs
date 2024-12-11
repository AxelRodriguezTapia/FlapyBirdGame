using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Necesario para interactuar con la UI
using TMPro;

public class AddScoreScript : MonoBehaviour
{
    public int score;  // La puntuación
    public TextMeshProUGUI scoreText;  // Referencia al componente Text en la UI

    void Start()
    {
        score = 0;
        UpdateScoreText();  // Muestra la puntuación inicial
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
}
