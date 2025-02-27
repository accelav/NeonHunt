using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpEnergiaMax : MonoBehaviour
{
    EnergyBarControl energyBarControl;
    void Start()
    {
        energyBarControl = GetComponent<EnergyBarControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        
    }

    private void OnDestroy()
    {
        energyBarControl.recargandoEnergia = false;
        energyBarControl.value = 100;
    }
}
