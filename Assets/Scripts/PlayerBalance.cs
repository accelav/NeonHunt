using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerBalance : MonoBehaviour
{
    public Rigidbody wheelRb;
    public Rigidbody bodyRb;

    // Par�metros para el PID o torque
    public float balanceForce = 100f;

    void FixedUpdate()
    {
        Vector3 upVector = bodyRb.transform.up;
        Vector3 targetUp = Vector3.up;

        // Error de inclinaci�n
        // Por simplicidad, medimos el �ngulo entre upVector y Vector3.up.
        float tiltAngle = Vector3.SignedAngle(upVector, targetUp, bodyRb.transform.right);

        // Aplica un torque proporcional al �ngulo de inclinaci�n para corregir
        // (puedes mejorarlo con un PID real: errorProporcional, integral, derivativo�)
        float correctiveTorque = tiltAngle * balanceForce;

        // Aplica el torque en el eje de rotaci�n adecuado (en este caso, el eje perpendicular a la rueda)
        // Asumiendo que la rueda gira alrededor del eje X, por ejemplo, aplicamos torque en Z para balancear
        bodyRb.AddTorque(transform.forward * -correctiveTorque, ForceMode.Force);
    }
}
