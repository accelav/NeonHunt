using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    /*public Transform player;          // El transform del jugador (padre de la c�mara)
    public float sensitivity = 2f;    // Sensibilidad del mouse
    public float minY = -30f;         // L�mite inferior de pitch
    public float maxY = 30f;          // L�mite superior de pitch
    public float rotationSpeed = 5f;  // Velocidad de suavizado para el jugador

    private float cameraYaw;
    private float playerYaw;
    private float pitch;

    void Start()
    {
        // Iniciar ambos yaw con el �ngulo Y actual del jugador.
        cameraYaw = player.eulerAngles.y;
        playerYaw = cameraYaw;
        // Tomar el pitch inicial a partir de la c�mara.
        pitch = transform.localEulerAngles.x;
    }

    void Update()
    {
        // 1. Obtener la entrada del rat�n
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // 2. Acumular el giro instant�neo de la c�mara
        cameraYaw += mouseX;
        pitch = Mathf.Clamp(pitch - mouseY, minY, maxY);

        // 3. Ajustar la rotaci�n local de la c�mara
        //    (el yaw local es la diferencia entre la c�mara y el jugador)
        transform.localRotation = Quaternion.Euler(pitch, cameraYaw - playerYaw, 0f);

        // 4. Suavizar la rotaci�n del jugador para que vaya �alcanzando� cameraYaw
        playerYaw = Mathf.LerpAngle(playerYaw, cameraYaw, Time.deltaTime * rotationSpeed);

        // 5. Aplicar ese yaw al jugador
        player.rotation = Quaternion.Euler(0f, playerYaw, 0f);
    }*/
}
