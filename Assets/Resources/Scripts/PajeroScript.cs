using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Necesario para interactuar con los UI elementos

public class PajeroScript : MonoBehaviour
{
    public float forceAmount = 0.2f; // Fuerza de impulso
    private Rigidbody2D rb; // Componente Rigidbody2D
    private Animator animator; // Componente Animator
    private Camera mainCamera; // Cámara principal
    private GameObject gameManager; // Game Manager
    public float maxTiltAngle = 45f; // Ángulo máximo de inclinación
    public float tiltSpeed = 2f; // Velocidad de inclinación
    private float screenWidth, screenHeight; // Límites de la pantalla
    private bool isDead; // Bandera de estado de muerte
    private bool gameStarted = false; // Bandera para determinar si el juego ha comenzado
    private float currentCooldownTime = 0f;
    public AudioClip[] pointSounds; // Lista de sonidos para los puntos
    private AudioSource audioSource; // Componente AudioSource

    void Start()
    {
        isDead = false;
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;
        screenWidth = mainCamera.orthographicSize * mainCamera.aspect;
        screenHeight = mainCamera.orthographicSize;
        audioSource = GetComponent<AudioSource>();
        // Desactivar física hasta que el juego comience
        rb.isKinematic = true;
    }

    void Update()
    {
        // Iniciar el juego al primer toque/clic
        if (!gameStarted && (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            gameStarted = true;
            rb.isKinematic = false; // Habilitar física
            animator.SetTrigger("Fly"); // Animación inicial
        }

        // Si el juego no ha comenzado, no ejecutar lógica adicional
        if (!gameStarted) return;

        // Comprobar si el jugador toca la pantalla para saltar
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) && !isDead)
        {
            if (rb != null)
            {
                rb.velocity = Vector2.zero; // Detener el objeto
                rb.AddForce(Vector2.up * forceAmount, ForceMode2D.Impulse); // Aplicar fuerza
                //animator.SetTrigger("Fly"); // Animación de salto
            }
        }

        // Verificar si el objeto ha salido de los límites de la cámara
        if (IsOutOfScreen())
        {
            isDead = true;
            animator.SetTrigger("Die");
        }

        if (isDead)
        {
            currentCooldownTime += Time.deltaTime;
            if (currentCooldownTime >= 3f)
            {
                Destroy(gameObject);
            }

        }

        // Controlar la rotación del pájaro
        HandleRotation();
    }

    bool IsOutOfScreen()
    {
        Vector3 screenPosition = mainCamera.WorldToViewportPoint(transform.position);
        return screenPosition.x < 0 || screenPosition.x > 1 || screenPosition.y < 0 || screenPosition.y > 1;
    }

    void HandleRotation()
    {
        if (rb.velocity.y < 0)
        {
            float tiltAngle = Mathf.LerpAngle(transform.eulerAngles.z, -maxTiltAngle, Time.deltaTime * tiltSpeed);
            transform.rotation = Quaternion.Euler(0, 0, tiltAngle);
        }
        else if (rb.velocity.y > 0)
        {
            float tiltAngle = Mathf.LerpAngle(transform.eulerAngles.z, maxTiltAngle, Time.deltaTime * tiltSpeed);
            transform.rotation = Quaternion.Euler(0, 0, tiltAngle);
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (!isDead)
        {
            gameManager.GetComponent<AddScoreScript>().sumScore();
        }
    }

    public void setDead(bool b)
    {
        isDead = b;
        audioSource.PlayOneShot(pointSounds[0]); // Reproducir el sonido
    }
}
