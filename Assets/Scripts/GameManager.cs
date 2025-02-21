using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool partidaEmpezada;
    public bool partidaPausada;
    public bool configurando;
    public bool restarting;
    public bool estaContando;
    public bool startButton = false;
    public bool estaDetectando = false;
    public bool estaMuerto = false;

    public float timer;


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

        partidaPausada = true;
        startButton = false;
    }

    private void Update()
    {

        if (estaContando)
        {
            timer = Time.deltaTime;
        }

        else
        {
            timer += 0;
        }

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
            partidaEmpezada = false;
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

}
