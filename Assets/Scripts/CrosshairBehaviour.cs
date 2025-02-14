using UnityEngine;

public class CrosshairBehaviour : MonoBehaviour
{
    public float maxDistance = 20f;
    public GameObject mirilla;
    [SerializeField] private Vector3 tama�oMaxMirilla;
    [SerializeField] private float timeAnim = 1f;
    [SerializeField] float sphereRadius = 2f;

    private Vector3 tama�oOriginal;

    private void Start()
    {
        tama�oOriginal = mirilla.transform.localScale;
    }

    private void Update()
    {
        // Calcula la direcci�n forward
        Vector3 direccion = transform.forward;

        // Realiza un SphereCastAll desde la posici�n actual hacia adelante
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, sphereRadius, direccion, maxDistance);
        bool enemigoDetectado = false;
        foreach (RaycastHit hit in hits)
        {
            //Debug.Log("SphereCastAll hit: " + hit.collider.gameObject.name);
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                enemigoDetectado = true;
                break;
            }
        }

        // Si detectamos al enemigo, iniciamos el tween solo si a�n no est� en curso
        if (enemigoDetectado)
        {
            if (!LeanTween.isTweening(mirilla))
            {
                LeanTween.scale(mirilla, tama�oMaxMirilla, timeAnim).setLoopPingPong();
                Debug.Log("Enemigo en el blanco");
            }
        }
        // Si ya no detectamos al enemigo, cancelamos el tween y restauramos la escala original
        else
        {
            if (LeanTween.isTweening(mirilla))
            {
                LeanTween.cancel(mirilla);
                mirilla.transform.localScale = tama�oOriginal;
                Debug.Log("Enemigo Perdido");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * maxDistance);
    }
}
