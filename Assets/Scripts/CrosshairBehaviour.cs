using UnityEngine;

public class CrosshairBehaviour : MonoBehaviour
{
    public float maxDistance = 20f;
    public GameObject mirilla;
    [SerializeField] private Vector3 tama�oMaxMirilla;
    [SerializeField] private float timeAnim = 1f;
    public bool enemigoDetectado = false;
    private Vector3 tama�oOriginal;

    public Transform enemigoDetectadoObj; // Usaremos Transform en lugar de GameObject para referencia precisa

    private void Start()
    {
        tama�oOriginal = mirilla.transform.localScale;
    }

    private void Update()
    {
        // Calcula la direcci�n forward
        Vector3 direccion = transform.forward;

        // Realiza un SphereCastAll desde la posici�n actual hacia adelante
        RaycastHit hit;
        enemigoDetectado = false;
        enemigoDetectadoObj = null;  // Restablecer al no detectar al enemigo
        Physics.Raycast(transform.position, direccion, out hit, maxDistance);
        if (hit.collider.CompareTag("Enemy"))
        {
            enemigoDetectado = true;
            enemigoDetectadoObj = hit.collider.transform;  // Guardamos el Transform del enemigo detectado

        }
        else
        {
            enemigoDetectado = false;
            enemigoDetectadoObj = null;
        }


        // Animaci�n de la mirilla
        if (enemigoDetectado)
        {
            if (!LeanTween.isTweening(mirilla))
            {
                LeanTween.scale(mirilla, tama�oMaxMirilla, timeAnim).setLoopPingPong();
                Debug.Log("Enemigo en el blanco");
            }
        }
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
