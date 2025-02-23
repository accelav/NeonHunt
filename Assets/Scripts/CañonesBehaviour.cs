using UnityEngine;
using UnityEngine.InputSystem;

public class CañonesBehaviour : MonoBehaviour
{
    private Animator animator;
    public Transform firePoint;
    public float fireRate = 0.5f;
    private float nextFireTime;
    public Camera cam;
    private Transform enemigoTransform;  // Referencia al transform del enemigo
    private CrosshairBehaviour crosshair;  // Referencia al script CrosshairBehaviour
    public Vector3 targetPosition;
    Bullet Bullet;
    [SerializeField]
    int puntosAlDisparar = -2;
    void Awake()
    {
        animator = GetComponent<Animator>();
        crosshair = FindObjectOfType<CrosshairBehaviour>();  // Encontrar el script de crosshair
        Bullet = GetComponent<Bullet>();
    }

    void OnClick(InputValue value)
    {
        if (GameManager.Instance.partidaPausada == true) return;
        if (value.isPressed && Time.time >= nextFireTime)
        {
            GameManager.Instance.OtorgarPuntos(puntosAlDisparar);
            animator.SetTrigger("Disparo");
            

            // Si hay un enemigo detectado
            if (crosshair != null && crosshair.enemigoDetectado)
            {
                // Disparamos hacia el enemigo
                GameObject bullet = BulletPool.Instance.GetBullet();
                bullet.transform.position = firePoint.position;

                // Usamos el Transform del padre del enemigo detectado
                enemigoTransform = crosshair.enemigoDetectadoObj.parent;  // Obtenemos el transform del padre

                // Disparo hacia el enemigo
                Vector3 shootDirection = -transform.up;//(enemigoTransform.position - firePoint.position).normalized;
                bullet.GetComponent<Bullet>().Initialize(shootDirection, enemigoTransform);

            }
            else
            {
                // Si no hay enemigo, disparar en línea recta
                GameObject bullet = BulletPool.Instance.GetBullet();
                bullet.transform.position = firePoint.position;

                // Disparo hacia el enemigo
                Vector3 shootDirection = -transform.up;//(enemigoTransform.position - firePoint.position).normalized;
                bullet.GetComponent<Bullet>().Initialize(shootDirection, null);

            }

            nextFireTime = Time.time + fireRate;
        }
    }
}
