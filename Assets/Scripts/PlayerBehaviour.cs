using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Camera cam;
    public float maxDistance;
    public GameObject mirilla;
    [SerializeField] private Vector3 tama�oMaxMirilla;
    [SerializeField] private float timeAnim;
    [SerializeField] float sphereRadius = 2f;
    // Estado para controlar si estamos apuntando a enemigo
    private bool hayEnemigo;
    private Vector3 tama�oOriginal;


    private void Start()
    {
        tama�oOriginal = mirilla.transform.localScale;
    }

    private void Update()
    {
        // Disparamos un ray desde la c�mara hacia delante
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        bool enemigoDetectado = false;
        if (Physics.SphereCast(ray, sphereRadius out hit, maxDistance))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                enemigoDetectado = true;
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
        else if (!enemigoDetectado && hayEnemigo)
        {
            hayEnemigo = false;
            LeanTween.cancel(mirilla);
            mirilla.transform.localScale = tama�oOriginal;
            Debug.Log("enemigo perdido -> cancelando animaci�n");
        }
    }
}
