using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerBalance : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("El Enemigo te ha detectado");
        }
;
    }
}
