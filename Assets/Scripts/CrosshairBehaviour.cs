using UnityEngine;

public class CrosshairBehaviour : MonoBehaviour
{
    public Camera cam;
    public float maxDistance = 20f;
    public GameObject mirilla;
    [SerializeField] private Vector3 tama�oMaxMirilla;
    [SerializeField] private float timeAnim = 1f;
    [SerializeField] float sphereRadius = 2f;
    // Estado para controlar si estamos apuntando a enemigo
    private bool hayEnemigo = false;
    private Vector3 tama�oOriginal;
    bool enemigoDetectado = false;

    private void Start()
    {
        tama�oOriginal = mirilla.transform.localScale;
        
    }

    private void Update()
    {
        // Disparamos un ray desde la c�mara hacia delante
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

        // Si encontramos un enemigo y todav�a no hab�amos empezado la animaci�n
        if (enemigoDetectado && !hayEnemigo)
        {
            hayEnemigo = true;
            LeanTween.scale(mirilla, tama�oMaxMirilla, timeAnim).setLoopPingPong();
            Debug.Log("enemigoDetectado -> iniciando animaci�n");
        }
        // Si dejamos de ver un enemigo y la animaci�n estaba activa
        /*else*/ if (!enemigoDetectado && hayEnemigo)
        {
            hayEnemigo = false;
            LeanTween.cancel(mirilla);
            mirilla.transform.localScale = tama�oOriginal;
            Debug.Log("enemigo perdido -> cancelando animaci�n");
        }
    }
}
