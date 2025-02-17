using UnityEngine;

public class CrosshairBehaviour : MonoBehaviour
{
    public float maxDistance = 20f;
    public GameObject mirilla;
    [SerializeField] private Vector3 tamañoMaxMirilla;
    [SerializeField] private float timeAnim = 1f;
    [SerializeField] float sphereRadius = 0.5f;  // Reducir el radio del SphereCast
    public bool enemigoDetectado = false;
    private Vector3 tamañoOriginal;

    public Transform enemigoDetectadoObj; // Usaremos Transform en lugar de GameObject para referencia precisa

    private void Start()
    {
        tamañoOriginal = mirilla.transform.localScale;
    }

    private void Update()
    {
        // Calcula la dirección forward
        Vector3 direccion = transform.forward;

        // Realiza un SphereCastAll desde la posición actual hacia adelante
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, sphereRadius, direccion, maxDistance);
        enemigoDetectado = false;
        enemigoDetectadoObj = null;  // Restablecer al no detectar al enemigo

        foreach (RaycastHit hit in hits)
        {
            // Filtramos solo objetos con el tag "Enemy"
            if (hit.collider.CompareTag("Enemy"))
            {
                enemigoDetectado = true;
                enemigoDetectadoObj = hit.collider.transform;  // Guardamos el Transform del enemigo detectado
                break; // Si encontramos un enemigo, ya no necesitamos continuar buscando
            }
        }

        // Animación de la mirilla
        if (enemigoDetectado)
        {
            if (!LeanTween.isTweening(mirilla))
            {
                LeanTween.scale(mirilla, tamañoMaxMirilla, timeAnim).setLoopPingPong();
                Debug.Log("Enemigo en el blanco");
            }
        }
        else
        {
            if (LeanTween.isTweening(mirilla))
            {
                LeanTween.cancel(mirilla);
                mirilla.transform.localScale = tamañoOriginal;
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
