using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float initialSpeed = 20f;  // Velocidad inicial de disparo
    public float homingStrength = 1f; // Cuánto ajusta su dirección hacia el objetivo
    private Rigidbody rb;
    private Transform target;         // Enemigo al que se dirige
    private Vector3 initialDirection; // Dirección inicial de disparo
    private float timeSinceShot = 0f; // Tiempo desde que se disparó

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;  // Desactivamos la gravedad
    }

    public void Initialize(Vector3 shootDirection, Transform enemyTarget)
    {
        target = enemyTarget;
        initialDirection = shootDirection.normalized; // Guardamos la dirección inicial
        rb.velocity = initialDirection * initialSpeed; // Disparo con velocidad inicial
        timeSinceShot = 0f; // Reiniciar el tiempo
    }

    void FixedUpdate()
    {
        timeSinceShot += Time.fixedDeltaTime;

        if (target == null)
        {
            rb.velocity = initialDirection * initialSpeed;
            transform.forward = rb.velocity.normalized;
        }

        else
        {
            // Dirección actual de la bala
            Vector3 currentDirection = rb.velocity.normalized;

            // Dirección hacia el objetivo
            Vector3 targetDirection = (target.position - transform.position).normalized;

            // Mezclamos la dirección inicial con la dirección al objetivo progresivamente
            float t = Mathf.Clamp01(timeSinceShot / 2f); // Controla la cantidad de curvatura con el tiempo
            Vector3 newDirection = Vector3.Lerp(initialDirection, targetDirection, t).normalized;

            // Aplicamos la nueva dirección sin perder velocidad
            rb.velocity = newDirection * initialSpeed;

            // Rotamos la bala para que mire hacia donde se mueve
            transform.forward = rb.velocity.normalized;
        }

        
    }

        private void OnCollisionEnter(Collision collision)
            {
                Destroy(gameObject);
            }
}
