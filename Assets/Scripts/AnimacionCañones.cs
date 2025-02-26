using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionCañones : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Disparar()
    {
        animator.SetTrigger("Disparo");
    }
}
