using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBarControl : MonoBehaviour
{
    Scrollbar scrollbar;
    public float value;
    public float valueDos;
    public bool canShoot;
    public bool recargandoEnergia;
    [SerializeField] float tiempoDeEspera;
    float timerDos;
    void Start()
    {
        scrollbar = GetComponent<Scrollbar>();
        scrollbar.size = 1;
        value = 1;
        canShoot = true;
        valueDos = value;
    }

    // Update is called once per frame
    void Update()
    {
        scrollbar.size = value;
        CanShoot();
        TimerShoot();
        Debug.Log(value);
        

    }
    public void HandleBar(float energyBar)
    {
        value += energyBar;

    }
    public void CanShoot()
    {
        
        if (value > 0.32f)
        {
            canShoot = true;
        }
          
        else
        {
            recargandoEnergia = true;
            canShoot = false;
        }
    }
    public void TimerShoot()
    {
        
        if (recargandoEnergia)
        {
            
            tiempoDeEspera += Time.deltaTime;
            if (tiempoDeEspera > 1)
            {
                valueDos = 0.00003f * tiempoDeEspera;
                HandleBar(valueDos);
                if (valueDos > 0.32f)
                {
                    tiempoDeEspera = 0;
                    recargandoEnergia = false;
                }
            }
        }

    }
}
