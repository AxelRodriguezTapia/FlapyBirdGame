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
    public TextMeshProUGUI PulsaParaEmpezarText;
    public TextMeshProUGUI bestScoreText;  // Texto para mostrar el mejor puntaje
    public GameObject restartButton; // El botón de reinicio
    public AudioClip[] pointSounds; // Lista de sonidos para los puntos
    public AudioSource audioSource; // Componente AudioSource

    private GameObject pajero;
    private bool startTrigger = true;
    private int bestScore;  // Variable para almacenar el mejor puntaje

    void Start()
    {
        score = 0;

        // Obtener el mejor puntaje almacenado en PlayerPrefs
        bestScore = PlayerPrefs.GetInt("BestScore", 0);

        // Actualizar la UI al inicio
        UpdateScoreText();
        UpdateBestScoreText();

        // Asegurarse de que el botón esté desactivado al inicio
        restartButton.SetActive(false);
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

        // Ocultar el texto inicial "Pulsa para empezar" después del primer toque
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) && startTrigger)
        {
            PulsaParaEmpezarText.text = "";
            startTrigger = false;
        }
    }

    // Método que se llama para sumar puntos
    public void sumScore()
    {
        score++;
        
        Debug.Log(score);
        UpdateScoreText();  // Actualiza el texto de la puntuación

        // Reproducir un sonido aleatorio
        PlayRandomPointSound();

        // Si el puntaje actual supera al mejor puntaje, actualizarlo
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore); // Guardar en PlayerPrefs
            UpdateBestScoreText(); // Actualizar la UI del mejor puntaje
        }
    }

    // Actualiza el texto de la puntuación en la UI
    void UpdateScoreText()
    {
        scoreText.text = "Puntuació: " + score.ToString();
    }

    // Actualiza el texto del mejor puntaje en la UI
    void UpdateBestScoreText()
    {
        bestScoreText.text = "Mejor Puntuació: " + bestScore.ToString();
    }

    // Reproducir un sonido aleatorio de la lista
    void PlayRandomPointSound()
    {
        if (pointSounds.Length > 0 && audioSource != null)
        {
            int randomIndex = Random.Range(0, pointSounds.Length); // Elegir un índice aleatorio
            audioSource.PlayOneShot(pointSounds[randomIndex]); // Reproducir el sonido
        }
    }

    // Método para reiniciar la escena
    public void RestartLevel()
    {
        // Obtener el nombre de la escena actual y recargarla
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
