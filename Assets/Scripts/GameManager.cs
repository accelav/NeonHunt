using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool partidaEmpezada = false;
    public bool partidaPausada = false;
    public bool configurando = false;
    public bool restarting = false;
    public bool estaContando = false;
    public bool startButton = false;
    public bool estaDetectando = false;
    public bool estaMuerto = false;

    public float timer;
    public int puntosTotales;
    public int puntosIniciales;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

        startButton = false;
        
    }

    private void Update()
    {
        

    }
    public void EmpezarPartida()
    {
        partidaEmpezada = true;
        partidaPausada = false;
        TimerOn();
        Time.timeScale = 1.0f;
        startButton = true;
    }

    public void PausarPartida()
    {
        partidaPausada = !partidaPausada;

        if (partidaPausada)
        {
            TimerOff();
            Time.timeScale = 0.0f;
        }
        if (partidaPausada && startButton == false)
        {
            Time.timeScale = 1.0f;
            if (Time.timeScale > 0.0f)
            {
                partidaPausada = true;
            }

        }

        if (partidaPausada == false && startButton == true)
        {
            EmpezarPartida();
        }

    }

    public void ReempezarPartida()
    {
        restarting = true;
        startButton = false;
        timer = 0.0f;
        ResetPuntos();
        SceneManager.LoadScene("GameScene");

    }
    public void TimerOn()
    {
        estaContando = true;
    }
    public void TimerOff()
    {
        estaContando = false;
    }

    public void OtorgarPuntos(int puntos)
    {
        puntosTotales += puntos;
    }

    public void ResetPuntos()
    {
        puntosTotales = 0;
    }
    void PuntosRecord()
    {
        PlayerPrefs.Save();
    }
}
