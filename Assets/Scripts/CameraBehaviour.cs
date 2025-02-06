using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraBehaviour : MonoBehaviour
{
    public Transform target;   // Objeto que rotar� en Y (ej. el jugador)
    public float sensitivity = 2f;  // Sensibilidad del mouse
    public float minY = -30f;  // L�mite inferior de la rotaci�n vertical
    public float maxY = 30f;   // L�mite superior de la rotaci�n vertical

    private float rotationX = 0f;

    void Update()
    {
        // Movimiento horizontal del mouse controla la rotaci�n Y del target
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        target.Rotate(Vector3.up * mouseX);

        // Movimiento vertical del mouse controla la rotaci�n X de la c�mara
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        rotationX = Mathf.Clamp(rotationX - mouseY, minY, maxY);
        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
}
