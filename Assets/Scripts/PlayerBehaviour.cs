using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 5f;

    // Variable que almacena el vector de movimiento (del composite que NO incluye S)
    private Vector2 movementVector = Vector2.zero;
    // Bandera que indica si se está frenando
    private bool isBraking = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Callback para el movimiento
    // Se supone que el composite usado en "Move" no tiene asignada la tecla S
    void OnMove(InputValue value)
    {
        // Solo se actualiza el movimiento si no se está frenando
        if (!isBraking)
        {
            movementVector = value.Get<Vector2>();
            // Para depuración:
            // Debug.Log("Movimiento recibido: " + movementVector);
        }
    }

    // Callback para el freno
    // Asegúrate de que en el Input Actions Asset la acción "Brake" tenga asignada únicamente la tecla S (o flecha down)
    void OnBrake(InputValue value)
    {
        isBraking = value.isPressed;
        if (isBraking)
        {
            // Cuando se activa el freno, se fuerza a cero tanto el vector de movimiento como la velocidad angular
            movementVector = Vector2.zero;
            rb.angularVelocity = Vector3.zero;
        }
        else
        {
            // Al soltar el freno, nos aseguramos de que no quede ningún valor residual.
            movementVector = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (isBraking)
        {
            // Mientras se mantiene el freno, se asegura que la rotación permanezca en cero.
            rb.angularVelocity = Vector3.zero;
        }
        else
        {
            // Se calcula el torque a aplicar basándose en el vector de movimiento.
            // Si movementVector está en cero, no se aplica torque.
            Vector3 torqueDirection = new Vector3(movementVector.y, 0, 0);
            transform.Rotate(torqueDirection * moveSpeed * Time.deltaTime);
        }
    }
}
