using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerBehaviour : MonoBehaviour
{
    public InputActionReference move;
    public Rigidbody rb;
    private Vector3 moveDirection;
    public float moveSpeed;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = move.action.ReadValue<Vector3>();


    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector3(x: moveDirection.x * moveSpeed, y: moveDirection.y * moveSpeed, z: moveDirection.z * moveSpeed);
    }
}
