using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField]
    float sphereCastRadius = 10f;
    [SerializeField]
    WaypointPatrol waypointPatrol;

    PlayerController playerController;

    // Nueva variable para saber si ya se detectó al jugador antes
    private bool estabaDetectando = false;

    private void Start()
    {
        waypointPatrol = GetComponent<WaypointPatrol>();
        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, sphereCastRadius);

        // Revisamos si el jugador está dentro del radio
        bool hayJugador = false;
        foreach (Collider col in hits)
        {
            if (col.CompareTag("Player"))
            {
                hayJugador = true;
                break;
            }
        }

        // Si encontramos al jugador y ANTES no estaba detectando, reproducimos sonido UNA sola vez
        if (hayJugador && !estabaDetectando)
        {
            SoundsBehaviour.instance.PlayDetectSound();
            waypointPatrol.followPlayer = true;
            GameManager.Instance.estaDetectando = true;
            estabaDetectando = true;
        }
        // Si ya no hay jugador y ANTES sí estaba detectando, reseteamos estados
        else if (!hayJugador && estabaDetectando)
        {
            waypointPatrol.followPlayer = false;
            GameManager.Instance.estaDetectando = false;
            estabaDetectando = false;
        }
        // En cualquier otro caso, no hacemos nada porque el estado no cambia
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sphereCastRadius);
    }
}
