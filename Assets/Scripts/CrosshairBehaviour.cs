using UnityEngine;

public class CrosshairBehaviour : MonoBehaviour
{
    public Camera cam;
    public float maxDistance = 20f;
    public GameObject mirilla;
    [SerializeField] private Vector3 tamañoMaxMirilla;
    [SerializeField] private float timeAnim = 1f;
    [SerializeField] float sphereRadius = 2f;
    // Estado para controlar si estamos apuntando a enemigo
    private bool hayEnemigo = false;
    private Vector3 tamañoOriginal;
    bool enemigoDetectado = false;

    private void Start()
    {
        tamañoOriginal = mirilla.transform.localScale;
        
    }

    private void Update()
    {
        // Disparamos un ray desde la cámara hacia delante
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        
        if (Physics.SphereCast(ray, sphereRadius, out hit, maxDistance))
        {
            Debug.Log("Rayo Colisionado");
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                enemigoDetectado = true;
            }
            else
            {
                enemigoDetectado = false;
            }

        }

        // Si encontramos un enemigo y todavía no habíamos empezado la animación
        if (enemigoDetectado && !hayEnemigo)
        {
            hayEnemigo = true;
            LeanTween.scale(mirilla, tamañoMaxMirilla, timeAnim).setLoopPingPong();
            Debug.Log("enemigoDetectado -> iniciando animación");
        }
        // Si dejamos de ver un enemigo y la animación estaba activa
        /*else*/ if (!enemigoDetectado && hayEnemigo)
        {
            hayEnemigo = false;
            LeanTween.cancel(mirilla);
            mirilla.transform.localScale = tamañoOriginal;
            Debug.Log("enemigo perdido -> cancelando animación");
        }
    }
}
