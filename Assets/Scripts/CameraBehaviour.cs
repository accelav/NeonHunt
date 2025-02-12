using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraBehaviour : MonoBehaviour
{
    public Transform target;          // Objeto que rota en Y (ej. el jugador)
    public float sensitivity = 2f;      // Sensibilidad del mouse
    public float minY = -30f;           // L�mite inferior de la rotaci�n vertical
    public float maxY = 30f;            // L�mite superior de la rotaci�n vertical

    private float rotationX = 0f;
    private float rotationY = 0f;
    public float smoothTime = 0.1f;     // Factor de suavizado

    private float lastMouseX;
    private float currentMouseX;

    void Update()
    {
        // Obtener los valores del mouse
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // Suavizar la entrada horizontal
        currentMouseX = Mathf.Lerp(lastMouseX, mouseX, smoothTime);
        lastMouseX = currentMouseX;

        // Acumula la rotaci�n horizontal y vertical
        target.transform.Rotate(Vector2.up * currentMouseX);
        rotationX = Mathf.Clamp(rotationX - mouseY, minY, maxY);

        // Aplica la rotaci�n combinada
        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }
}
