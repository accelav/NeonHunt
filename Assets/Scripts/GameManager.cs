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
    public int enemigosTotales;
    public bool esperarParDisparar = false;

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
            return;
        }

        // Si quieres que desde el principio esté el cursor bloqueado:
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        // Por si acaso
        startButton = false;
    }

    private void Update()
    {
        // Cada vez que se pulse Escape, se alterna la pausa
        /*if (Input.GetKeyUp(KeyCode.Escape))
        {
            PausarPartida();
        }*/
        /*if (enemigosTotales == 0)
        {
            estaMuerto = true;
        }*/

    }

    public void EmpezarPartida()
    {
        partidaEmpezada = true;
        partidaPausada = false;
        startButton = true;

        // Activa el timer y reanuda el tiempo
        TimerOn();
        Time.timeScale = 1.0f;

        // Bloquea y oculta el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PausarPartida()
    {
        // Alternamos el estado de pausa
        partidaPausada = !partidaPausada;

        if (partidaPausada)
        {
            // Si está pausado, se detiene el tiempo y el contador
            TimerOff();
            Time.timeScale = 0.0f;

            // Desbloquear y mostrar cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            // Si se reanuda y la partida había empezado
            if (partidaEmpezada)
            {
                TimerOn();
                Time.timeScale = 1.0f;

                // Bloquear y ocultar cursor
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    public void ReempezarPartida()
    {
        restarting = true;
        startButton = false;
        timer = 0.0f;
        ResetPuntos();
        estaMuerto = false;
        // Carga la escena de nuevo
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

    public void EnemiesCounter(int enemigos)
    {
        enemigosTotales += enemigos;
    }
}
