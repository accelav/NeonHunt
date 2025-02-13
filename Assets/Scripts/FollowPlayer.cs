using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    // Objeto que queremos seguir
    public Transform target;
    // Velocidad de seguimiento
    public float smoothSpeed = 5f;
    // Offset opcional para mantener una distancia o posici�n relativa
    public Vector3 offset;

    void LateUpdate()
    {
        if (target != null)
        {
            // Calcula la posici�n deseada basada en el target y el offset
            Vector3 desiredPosition = target.position + offset;
            // Interpola suavemente la posici�n actual a la posici�n deseada
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}

