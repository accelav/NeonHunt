using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class CañonesBehaviour : MonoBehaviour
{
    StarterAssetsInputs starterInput;
    AnimacionCañones animacionCañones;
    public Transform firePoint;
    public Transform firePointTwo;
    public float fireRate = 0.5f;
    private float nextFireTime;
    public Camera cam;
    private Transform enemigoTransform;  // Referencia al transform del enemigo
    private CrosshairBehaviour crosshair;  // Referencia al script CrosshairBehaviour
    public Vector3 targetPosition;
    [SerializeField]
    int puntosAlDisparar = -2;

    public BulletPool pool;
    CameraShake CameraShake;
    void Awake()
    {
        animacionCañones = FindAnyObjectByType<AnimacionCañones>();
        crosshair = FindObjectOfType<CrosshairBehaviour>();  // Encontrar el script de crosshair

        //pool = GetComponent<BulletPool>();
        starterInput = FindAnyObjectByType<StarterAssetsInputs>();
        CameraShake = FindObjectOfType<CameraShake>();
    }
    private void Update()
    {
        GameManager.Instance.esperarParDisparar = Time.time < nextFireTime;
        FireTrigger();
        starterInput.fire = false;

    }

    void FireTrigger()
    {
        if (GameManager.Instance.partidaPausada || GameManager.Instance.estaMuerto || !GameManager.Instance.partidaEmpezada || GameManager.Instance.hasGanado)
        {

        }
        else
        {
            if (starterInput.fire && Time.time >= nextFireTime)
            {
                GameManager.Instance.OtorgarPuntos(puntosAlDisparar);
                animacionCañones.Disparar();
                CameraShake.Shake();

                // Si hay un enemigo detectado
                if (crosshair != null && crosshair.enemigoDetectado)
                {
                    // Disparamos hacia el enemigo
                    GameObject bullet = pool.GetElementFromPool();
                    GameObject bulletTwo = pool.GetElementFromPool();
                    bullet.SetActive(true);
                    bulletTwo.SetActive(true);
                    bullet.transform.position = firePoint.position;
                    bulletTwo.transform.position = firePointTwo.position;

                    // Usamos el Transform del padre del enemigo detectado
                    enemigoTransform = crosshair.enemigoDetectadoObj.parent;  // Obtenemos el transform del padre

                    // Disparo hacia el enemigo
                    Vector3 shootDirection = -transform.up;
                    bullet.GetComponent<Bullet>().Initialize(shootDirection, enemigoTransform);
                    bulletTwo.GetComponent<Bullet>().Initialize(shootDirection, enemigoTransform);
                    starterInput.fire = false;
                }
                else
                {
                    // Si no hay enemigo, disparar en línea recta
                    GameObject bullet = pool.GetElementFromPool();
                    GameObject bulletTwo = pool.GetElementFromPool();
                    bullet.SetActive(true);
                    bulletTwo.SetActive(true);
                    bullet.transform.position = firePoint.position;
                    bulletTwo.transform.position = firePointTwo.position;


                    // Disparo hacia el enemigo
                    Vector3 shootDirection = -transform.up;

                    bullet.GetComponent<Bullet>().Initialize(shootDirection, null);
                    bulletTwo.GetComponent<Bullet>().Initialize(shootDirection, null);
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
  
}
