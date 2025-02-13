using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class CañonesBehaviour : MonoBehaviour
{
    private Animator animator;
    float trigger;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    void OnClick(InputValue value)
    {

        if (value.isPressed)
        {
            animator.SetTrigger("Disparo");
        }

    }
}
