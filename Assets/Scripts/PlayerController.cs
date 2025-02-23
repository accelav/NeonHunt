
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Camera References")]
    public Transform cameraTransform;     // Cámara, hija del jugador

    [Header("Movement Settings")]
    public float speed = 10f;            // Velocidad W/S
    public float acceleration = 5f;      // Suavizado de velocidad
    public float rotationSmooth = 5f;    // Suavizado al rotar el jugador para “seguir” a la cámara

    [Header("Mouse Look Settings")]
    public float mouseSensitivity = 2f;  // Sensibilidad del ratón
    public float minPitch = -30f;        // Límite inferior
    public float maxPitch = 30f;         // Límite superior

    [Header("Key Rotation Settings (A/D)")]
    public float turnSpeed = 100f;       // Velocidad de rotación horizontal al mantener A/D

    [Header("Wheel Rotation")]
    public Transform wheelTransform;     // Rueda (u otro objeto) que se quiere rotar según la velocidad

    [Header("Animations")]

    Animator animator;
    // Variables de entrada (Nuevo Input System)
    [SerializeField]
    private Vector2 moveInput;           // W/S/A/D
    [SerializeField]
    private Vector2 lookInput;           // Ratón

    // Rotaciones internas
    private float cameraYaw;  // “Yaw de la cámara” (suma ratón + A/D)
    private float playerYaw;  // Yaw real del jugador (se suaviza)
    private float pitch;      // Rotación vertical de la cámara (x)

    private Rigidbody rb;

    public float gravitySpeed;

    LayerMask layerMask;
    public bool rayGrounded;
    [SerializeField]
    float offsetGrounded;


    private float currentTiltX = 0f;
    private float currentTiltZ = 0f;
    public float smoothSpeed = 10f; // Ajusta este valor para controlar la velocidad de la transición

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        animator = GetComponent<Animator>();

        // Tomar la rotación inicial
        cameraYaw = transform.eulerAngles.y;
        playerYaw = cameraYaw;
        pitch = cameraTransform.localEulerAngles.x;
        animator = GetComponent<Animator>();
    }

    // ------------------------------
    // NUEVO INPUT SYSTEM: CALLBACKS
    // ------------------------------
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log("moveInput " + moveInput);
    }

    void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
        Debug.Log(lookInput);
    }

    // ------------------------------
    // UNITY LOOP
    // ------------------------------
    void Update()
    {
        HandleMouseLook();
        HandleKeyRotationContinuous();
        SmoothRotatePlayer();
        CheckGround();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    // ------------------------------
    // 1) CONTROL DE LA MIRADA (MOUSE) EN PITCH/YAW
    // ------------------------------
    private void HandleMouseLook()
    {
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        cameraYaw += mouseX;

        pitch = Mathf.Clamp(pitch - mouseY, minPitch, maxPitch);
        cameraTransform.localRotation = Quaternion.Euler(pitch, cameraYaw, 0f);
    }

    // ------------------------------
    // 2) ROTACIÓN CONTINUA CON A/D
    // ------------------------------
    private void HandleKeyRotationContinuous()
    {
        // moveInput.x -> A/D
        // Si es > 0, rotar a la derecha; si < 0, rotar a la izquierda
        float horizontal = moveInput.x;

        // Añadir rotación (en grados/segundo) al cameraYaw
        // Mientras más tiempo esté pulsado, más gira
        cameraYaw += horizontal * turnSpeed * Time.deltaTime;


    }

    // ------------------------------
    // 3) SUAVIZAR LA ROTACIÓN DEL JUGADOR HACIA cameraYaw
    // ------------------------------
    private void SmoothRotatePlayer()
    {
        playerYaw = Mathf.LerpAngle(playerYaw, cameraYaw, rotationSmooth * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0f, playerYaw, 0f);
    }

    // ------------------------------
    // 4) MOVER AL JUGADOR (W/S) SEGÚN LA CÁMARA
    // ------------------------------
    private void MovePlayer()
    {

        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = cameraTransform.right;
        camRight.y = 0f;
        camRight.Normalize();

        Physics.gravity = new Vector3(0, gravitySpeed, 0); // La gravedad debe ser negativa para que actúe hacia abajo

        // Calcula la velocidad horizontal actual y la deseada (sin la componente Y)
        Vector3 currentHorizontal = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        Vector3 targetHorizontal = camForward * moveInput.y * speed;

        // Suaviza solo las componentes horizontales
        Vector3 newHorizontal = Vector3.Lerp(currentHorizontal, targetHorizontal, acceleration * Time.fixedDeltaTime);

        // Actualiza la velocidad del Rigidbody combinando el nuevo vector horizontal con la velocidad vertical actual
        rb.velocity = new Vector3(newHorizontal.x, rb.velocity.y, newHorizontal.z);

        // Define los valores objetivo a partir del input
        float targetTiltX = -moveInput.y; // O cualquier factor que necesites
        float targetTiltZ = moveInput.x;

        // Interpola suavemente desde el valor actual al objetivo
        currentTiltX = Mathf.Lerp(currentTiltX, targetTiltX, smoothSpeed * Time.deltaTime);
        currentTiltZ = Mathf.Lerp(currentTiltZ, targetTiltZ, smoothSpeed * Time.deltaTime);

        // Envía los valores al Animator
        animator.SetFloat("TiltX", currentTiltX);
        animator.SetFloat("TiltZ", currentTiltZ);

        // 5) ROTAR LA RUEDA SEGÚN LA VELOCIDAD
        // ------------------------------
        if (wheelTransform != null)
        {
            // Ejemplo básico: rotar la rueda en el eje X según la magnitud de la velocidad.
            // Ajusta el factor (p.e. 5f) según el tamaño de la rueda o la sensación que desees.
            float rotationAmount = rb.velocity.magnitude * 20f * Time.fixedDeltaTime;
            wheelTransform.Rotate(rotationAmount, 0f, 0f);

        }
    }
    void LateUpdate()
    {
        // Conserva la rotación animada en X y Z,
        // pero fuerza la rotación en Y según el control del script.
        Vector3 currentEuler = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(currentEuler.x, playerYaw, currentEuler.z);
    }

    private void CheckGround()
    {
        Vector3 posicion = new Vector3(transform.position.x, transform.position.y - offsetGrounded, transform.position.z);
        rayGrounded = Physics.CheckSphere(posicion, 0.3f , layerMask , QueryTriggerInteraction.Ignore);
      
    }
}
