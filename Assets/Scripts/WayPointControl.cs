using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;

    public int m_CurrentWaypointIndex;

    private PlayerController playerController;
    public Vector3 currentPlayerPosition;
    public Vector3 realWaypoint;
    public bool followPlayer = false;
    public bool haChocado = false;

    void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
        playerController = FindAnyObjectByType<PlayerController>();
        // Si 'realWaypoint' guarda alguna posici�n en particular, in�ciala aqu�
        realWaypoint = waypoints[2].position;
    }

    void Update()
    {
        // Si la partida est� pausada o el jugador est� muerto, no hacemos nada
        if (GameManager.Instance.partidaPausada || GameManager.Instance.estaMuerto || !GameManager.Instance.partidaEmpezada || GameManager.Instance.hasGanado)
        {

        }
        else
        {
            currentPlayerPosition = playerController.transform.position;

            // Si seguimos al jugador, el destino ser� su posici�n
            if (followPlayer)
            {
                waypoints[2].position = currentPlayerPosition;
                navMeshAgent.SetDestination(waypoints[2].position);
            }
            // Si no seguimos al jugador, movemos el agente entre waypoints
            else
            {
                // Aseg�rate de resetear la posici�n del waypoint[1] a algo deseado
                waypoints[1].position = realWaypoint;

                // Cuando llega al waypoint, pasamos al siguiente
                if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
                {
                    m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
                    navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                }
            }
        }
        
    }

    /// <summary>
    /// M�todo llamado cuando el agente colisiona con algo que no sea "Trigger".
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        // Verificamos si lo que hemos tocado tiene la etiqueta "Wall"
        if (collision.gameObject.CompareTag("Pared"))
        {
            // Podemos forzar la elecci�n de un waypoint aleatorio distinto
            haChocado = true;
            CambiarDireccion();
        }
    }

    /// <summary>
    /// M�todo para cambiar de direcci�n a un waypoint aleatorio
    /// </summary>
    private void CambiarDireccion()
    {
        // Elige un �ndice aleatorio en el rango de tus waypoints
        int randomIndex = Random.Range(0, waypoints.Length);
        if (navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.SetDestination(waypoints[0].position);
        }
        else
        {
            Debug.LogWarning("NavMeshAgent no est� sobre un NavMesh");
        }

        // Si deseas que el enemigo deje de 'seguir al jugador' cuando choca:
        followPlayer = false;

        // Si quieres hacer m�s l�gica (por ejemplo, evitar que se elija el
        // mismo waypoint actual), puedes a�adir checks extra aqu�
    }
}
