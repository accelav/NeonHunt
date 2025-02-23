
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Camera References")]
    public Transform cameraTransform;     // C�mara, hija del jugador

    [Header("Movement Settings")]
    public float speed = 10f;            // Velocidad W/S
    public float acceleration = 5f;      // Suavizado de velocidad
    public float rotationSmooth = 5f;    // Suavizado al rotar el jugador para �seguir� a la c�mara

    [Header("Mouse Look Settings")]
    public float mouseSensitivity = 2f;  // Sensibilidad del rat�n
    public float minPitch = -30f;        // L�mite inferior
    public float maxPitch = 30f;         // L�mite superior

    [Header("Key Rotation Settings (A/D)")]
    public float turnSpeed = 100f;       // Velocidad de rotaci�n horizontal al mantener A/D

    [Header("Wheel Rotation")]
    public Transform wheelTransform;     // Rueda (u otro objeto) que se quiere rotar seg�n la velocidad

    [Header("Animations")]

    Animator animator;
    // Variables de entrada (Nuevo Input System)
    [SerializeField]
    private Vector2 moveInput;           // W/S/A/D
    [SerializeField]
    private Vector2 lookInput;           // Rat�n

    // Rotaciones internas
    private float cameraYaw;  // �Yaw de la c�mara� (suma rat�n + A/D)
    private float playerYaw;  // Yaw real del jugador (se suaviza)
    private float pitch;      // Rotaci�n vertical de la c�mara (x)

    private Rigidbody rb;

    public float gravitySpeed;

    LayerMask layerMask;
    public bool rayGrounded;
    [SerializeField]
    float offsetGrounded;


    private float currentTiltX = 0f;
    private float currentTiltZ = 0f;
    public float smoothSpeed = 10f; // Ajusta este valor para controlar la velocidad de la transici�n

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        animator = GetComponent<Animator>();

        // Tomar la rotaci�n inicial
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
    // 2) ROTACI�N CONTINUA CON A/D
    // ------------------------------
    private void HandleKeyRotationContinuous()
    {
        // moveInput.x -> A/D
        // Si es > 0, rotar a la derecha; si < 0, rotar a la izquierda
        float horizontal = moveInput.x;

        // A�adir rotaci�n (en grados/segundo) al cameraYaw
        // Mientras m�s tiempo est� pulsado, m�s gira
        cameraYaw += horizontal * turnSpeed * Time.deltaTime;


    }

    // ------------------------------
    // 3) SUAVIZAR LA ROTACI�N DEL JUGADOR HACIA cameraYaw
    // ------------------------------
    private void SmoothRotatePlayer()
    {
        playerYaw = Mathf.LerpAngle(playerYaw, cameraYaw, rotationSmooth * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0f, playerYaw, 0f);
    }

    // ------------------------------
    // 4) MOVER AL JUGADOR (W/S) SEG�N LA C�MARA
    // ------------------------------
    private void MovePlayer()
    {

        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = cameraTransform.right;
        camRight.y = 0f;
        camRight.Normalize();

        Physics.gravity = new Vector3(0, gravitySpeed, 0); // La gravedad debe ser negativa para que act�e hacia abajo

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

        // Env�a los valores al Animator
        animator.SetFloat("TiltX", currentTiltX);
        animator.SetFloat("TiltZ", currentTiltZ);

        // 5) ROTAR LA RUEDA SEG�N LA VELOCIDAD
        // ------------------------------
        if (wheelTransform != null)
        {
            // Ejemplo b�sico: rotar la rueda en el eje X seg�n la magnitud de la velocidad.
            // Ajusta el factor (p.e. 5f) seg�n el tama�o de la rueda o la sensaci�n que desees.
            float rotationAmount = rb.velocity.magnitude * 20f * Time.fixedDeltaTime;
            wheelTransform.Rotate(rotationAmount, 0f, 0f);

        }
    }
    void LateUpdate()
    {
        // Conserva la rotaci�n animada en X y Z,
        // pero fuerza la rotaci�n en Y seg�n el control del script.
        Vector3 currentEuler = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(currentEuler.x, playerYaw, currentEuler.z);
    }

    private void CheckGround()
    {
        Vector3 posicion = new Vector3(transform.position.x, transform.position.y - offsetGrounded, transform.position.z);
        rayGrounded = Physics.CheckSphere(posicion, 0.3f , layerMask , QueryTriggerInteraction.Ignore);
      
    }
}
