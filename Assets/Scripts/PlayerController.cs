using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 10f;
    public float speedSmother = 5f;
    private Vector2 movement;

    public bool isGrounded = false;

    public Transform wheelTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void OnMove(InputValue movementValue)
    {
        movement = movementValue.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        MoverPlayer();
    }

    void MoverPlayer()
    {


        // Obtener las direcciones de la cámara (forward y right) sin la componente vertical.
        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = Camera.main.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        // Calcular la velocidad objetivo en función de la entrada y la dirección de la cámara.
        Vector3 targetVelocity = (camForward * movement.y + camRight * movement.x) * speed;
        // Mantener la componente vertical para que la gravedad siga actuando.
        targetVelocity.y = rb.velocity.y;

        // Suavizar la transición entre la velocidad actual y la velocidad objetivo.
        rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, speedSmother * Time.deltaTime);


        wheelTransform.transform.Rotate(rb.velocity.z, 0, 0);   
    }
}
