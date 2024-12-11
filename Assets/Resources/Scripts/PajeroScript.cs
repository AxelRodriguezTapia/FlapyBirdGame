using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Necesario para interactuar con los UI elementos

public class PajeroScript : MonoBehaviour
{
    // La fuerza que se aplicará al objeto
    public float forceAmount = 0.2f;

    // Componente Rigidbody2D del objeto
    private Rigidbody2D rb;

    private float currentCooldownTime = 0f;

    // Componente Animator para la animación del pájaro
    private Animator animator;

    // Variables para la cámara
    private Camera mainCamera;
    private float screenWidth;
    private float screenHeight;

    private GameObject gameManager;

    // Variables para la rotación
    public float maxTiltAngle = 45f;  // Ángulo máximo de inclinación
    public float tiltSpeed = 2f;  // Velocidad de inclinación

    private bool isDead;


    void Start()
    {
        isDead = false;
        // Obtener el Rigidbody2D del objeto al que está adjunto el script
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindGameObjectWithTag("GameController");

        // Obtener el componente Animator del objeto (para controlar la animación)
        animator = GetComponent<Animator>();

        // Obtener la cámara principal
        mainCamera = Camera.main;

        // Calcular los límites de la pantalla en unidades del mundo
        screenWidth = mainCamera.orthographicSize * mainCamera.aspect;
        screenHeight = mainCamera.orthographicSize;
    }

    void Update()
    {
        // Comprobar si el jugador toca la pantalla (o hace clic)
        if (Input.GetMouseButtonDown(0) && !isDead) // 0 significa clic izquierdo o primer toque
        {
            // Verificar que el objeto tiene un Rigidbody2D
            if (rb != null)
            {
                // Poner la velocidad a 0 (detener el objeto antes de aplicar la fuerza)
                rb.velocity = Vector2.zero;

                // Calcular la dirección hacia la que se aplicará la fuerza
                Vector2 forceDirection = Vector2.up; // Aplicamos la fuerza hacia arriba

                // Aplicar la fuerza al objeto
                rb.AddForce(forceDirection * forceAmount, ForceMode2D.Impulse);

                // Activar la animación de "salto" o "vuelo"
                animator.SetTrigger("Fly"); // Asegúrate de que tienes un Trigger llamado "Fly" en el Animator
            }
        }

        // Verificar si el objeto ha salido de los límites de la cámara
        if (IsOutOfScreen())
        {
            // Destruir el objeto si ha salido de la pantalla
            isDead=true;
            animator.SetTrigger("Die");
        }

        if (isDead)
        {
            // Sumar el tiempo transcurrido
            currentCooldownTime += Time.deltaTime;

            // Comprobar si han pasado los 3 segundos
            if (currentCooldownTime >= 3f)
            {
                Destroy(gameObject);
            }
        }

        // Controlar la rotación del pájaro
        HandleRotation();
    }

    // Función que determina si el objeto ha salido de la pantalla
    bool IsOutOfScreen()
    {
        // Obtener la posición del objeto en el mundo
        Vector3 screenPosition = mainCamera.WorldToViewportPoint(transform.position);

        // Comprobar si la posición está fuera de los límites visibles de la cámara
        return screenPosition.x < 0 || screenPosition.x > 1 || screenPosition.y < 0 || screenPosition.y > 1;
    }

    // Método para controlar la rotación del pájaro
    void HandleRotation()
    {
        // Si el pájaro está cayendo (velocidad negativa en el eje Y), rota hacia abajo
        if (rb.velocity.y < 0)
        {
            // Rota hacia abajo, dentro de un rango
            float tiltAngle = Mathf.LerpAngle(transform.eulerAngles.z, -maxTiltAngle, Time.deltaTime * tiltSpeed);
            transform.rotation = Quaternion.Euler(0, 0, tiltAngle);
        }
        // Si el pájaro está subiendo (velocidad positiva en el eje Y), rota hacia arriba
        else if (rb.velocity.y > 0)
        {
            // Rota hacia arriba, dentro de un rango
            float tiltAngle = Mathf.LerpAngle(transform.eulerAngles.z, maxTiltAngle, Time.deltaTime * tiltSpeed);
            transform.rotation = Quaternion.Euler(0, 0, tiltAngle);
        }
    }

    // Método de colisión
    void OnTriggerExit2D(Collider2D collider2D)
    {
        gameManager.GetComponent<AddScoreScript>().sumScore();
    }

    public void setDead(bool b)
    {
        isDead = b;
    }
}
