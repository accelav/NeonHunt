using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class CañonesBehaviour : MonoBehaviour
{
    StarterAssetsInputs starterInput;
    private Animator animator;
    public Transform firePoint;
    public float fireRate = 0.5f;
    private float nextFireTime;
    public Camera cam;
    private Transform enemigoTransform;  // Referencia al transform del enemigo
    private CrosshairBehaviour crosshair;  // Referencia al script CrosshairBehaviour
    public Vector3 targetPosition;
    Bullet Bullet;
    bool esperarParaDisparar;
    [SerializeField]
    int puntosAlDisparar = -2;

    public BulletPool pool;

    void Awake()
    {
        animator = GetComponent<Animator>();
        crosshair = FindObjectOfType<CrosshairBehaviour>();  // Encontrar el script de crosshair
        Bullet = GetComponent<Bullet>();
        //pool = GetComponent<BulletPool>();
        starterInput = FindAnyObjectByType<StarterAssetsInputs>();
    }
    private void Update()
    {
        GameManager.Instance.esperarParDisparar = Time.time < nextFireTime;
        FireTrigger();
        starterInput.fire = false;

    }

    void FireTrigger()
    {
        if (GameManager.Instance.partidaPausada == true) return;

        if (starterInput.fire && Time.time >= nextFireTime)
        {
            GameManager.Instance.OtorgarPuntos(puntosAlDisparar);
            animator.SetTrigger("Disparo");


            // Si hay un enemigo detectado
            if (crosshair != null && crosshair.enemigoDetectado)
            {
                // Disparamos hacia el enemigo
                GameObject bullet = pool.GetElementFromPool();
                bullet.SetActive(true);
                bullet.transform.position = firePoint.position;

                // Usamos el Transform del padre del enemigo detectado
                enemigoTransform = crosshair.enemigoDetectadoObj.parent;  // Obtenemos el transform del padre

                // Disparo hacia el enemigo
                Vector3 shootDirection = -transform.up;
                bullet.GetComponent<Bullet>().Initialize(shootDirection, enemigoTransform);
                starterInput.fire = false;
            }
            else
            {
                // Si no hay enemigo, disparar en línea recta
                GameObject bullet = pool.GetElementFromPool();
                bullet.SetActive(true);
                bullet.transform.position = firePoint.position;

                // Disparo hacia el enemigo
                Vector3 shootDirection = -transform.up;

                bullet.GetComponent<Bullet>().Initialize(shootDirection, null);
                starterInput.fire = false;
            }

            nextFireTime = Time.time + fireRate;
            
        }
        if (starterInput.fire && Time.time < nextFireTime)
        {
            SoundsBehaviour.instance.PlayRecargaSound();
        }
    }
  
}
