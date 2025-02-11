using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Rigidbody of the player.
    private Rigidbody rb;

    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

    // Speed at which the player moves.
    public float speed = 0;

    public WheelCollider wheelCollider;

    public float motorTorque = 1500f;

    private Transform wheelTransform;

    float movement;


    // Start is called before the first frame update.
    void Start()
    {
        // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();
        wheelTransform = GetComponent<Transform>();
    }

    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        //Vector2 movementVector = movementValue.Get<Vector2>();
        movement = movementValue.Get<float>();

        
        // Store the X and Y components of the movement.
        //movementX = movement.x;
        //movementY = movement.y;
    }

    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate()
    {
        // Create a 3D movement vector using the X and Y inputs.

        // Apply force to the Rigidbody to move the player.
        wheelCollider.motorTorque = motorTorque * movement;

        UpdateWheelPose();
    }

    void UpdateWheelPose()
    {
        Vector3 pos;
        Quaternion quat;
        wheelCollider.GetWorldPose(out pos, out quat);
        wheelTransform.position = pos;
        wheelTransform.rotation = quat;
    }
}
