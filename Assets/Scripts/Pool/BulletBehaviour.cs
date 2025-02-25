using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float initialSpeed = 20f;  // Velocidad inicial de disparo
    public float homingStrength = 1f; // Cu�nto ajusta su direcci�n hacia el objetivo
    private Rigidbody rb;
    private Transform target;         // Enemigo al que se dirige
    private Vector3 initialDirection; // Direcci�n inicial de disparo
    private float timeSinceShot = 0f; // Tiempo desde que se dispar�
    public BulletPool pool;
    float timer = 5f;
    public GameObject particles;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;  // Desactivamos la gravedad
        
    }

    public void Initialize(Vector3 shootDirection, Transform enemyTarget)
    {
        target = enemyTarget;
        initialDirection = shootDirection.normalized; // Guardamos la direcci�n inicial
        rb.velocity = initialDirection * initialSpeed; // Disparo con velocidad inicial
        timeSinceShot = 0f; // Reiniciar el tiempo
    }

    void FixedUpdate()
    {
        
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = 5f;
            pool.ReturnToPool(gameObject);
        }
    
    timeSinceShot += Time.fixedDeltaTime;

        if (target == null)
        {
            rb.velocity = initialDirection * initialSpeed;
            transform.forward = rb.velocity.normalized;
        }

        else
        {
            // Direcci�n actual de la bala
            Vector3 currentDirection = rb.velocity.normalized;

            // Direcci�n hacia el objetivo
            Vector3 targetDirection = (target.position - transform.position).normalized;

            // Mezclamos la direcci�n inicial con la direcci�n al objetivo progresivamente
            float t = Mathf.Clamp01(timeSinceShot / 2f); // Controla la cantidad de curvatura con el tiempo
            Vector3 newDirection = Vector3.Lerp(initialDirection, targetDirection, t).normalized;

            // Aplicamos la nueva direcci�n sin perder velocidad
            rb.velocity = newDirection * initialSpeed;

            // Rotamos la bala para que mire hacia donde se mueve
            transform.forward = rb.velocity.normalized;
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 posicion = gameObject.transform.position;
        GameObject particulas = Instantiate(particles,new Vector3(posicion.x, posicion.y, posicion.z), Quaternion.identity);
        ReturnToPool();
    }

    void ReturnToPool()
    {
        pool.ReturnToPool(gameObject);
    }
}
