using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    /*public Transform player;          // El transform del jugador (padre de la cámara)
    public float sensitivity = 2f;    // Sensibilidad del mouse
    public float minY = -30f;         // Límite inferior de pitch
    public float maxY = 30f;          // Límite superior de pitch
    public float rotationSpeed = 5f;  // Velocidad de suavizado para el jugador

    private float cameraYaw;
    private float playerYaw;
    private float pitch;

    void Start()
    {
        // Iniciar ambos yaw con el ángulo Y actual del jugador.
        cameraYaw = player.eulerAngles.y;
        playerYaw = cameraYaw;
        // Tomar el pitch inicial a partir de la cámara.
        pitch = transform.localEulerAngles.x;
    }

    void Update()
    {
        // 1. Obtener la entrada del ratón
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // 2. Acumular el giro instantáneo de la cámara
        cameraYaw += mouseX;
        pitch = Mathf.Clamp(pitch - mouseY, minY, maxY);

        // 3. Ajustar la rotación local de la cámara
        //    (el yaw local es la diferencia entre la cámara y el jugador)
        transform.localRotation = Quaternion.Euler(pitch, cameraYaw - playerYaw, 0f);

        // 4. Suavizar la rotación del jugador para que vaya “alcanzando” cameraYaw
        playerYaw = Mathf.LerpAngle(playerYaw, cameraYaw, Time.deltaTime * rotationSpeed);

        // 5. Aplicar ese yaw al jugador
        player.rotation = Quaternion.Euler(0f, playerYaw, 0f);
    }*/
}
