using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField]
    float sphereCastRadius = 10f;
    [SerializeField]
    WaypointPatrol waypointPatrol;
    PlayerController playerController;
    
    private void Start()
    {
        waypointPatrol = GetComponent<WaypointPatrol>();
        playerController = FindAnyObjectByType<PlayerController>();
    }
    private void Update()
    {

        Collider[] hits = Physics.OverlapSphere(transform.position, sphereCastRadius);

        waypointPatrol.followPlayer = false;
        GameManager.Instance.estaDetectando = false;

        foreach (Collider col in hits)
        {
            if (col.CompareTag("Player"))
            {
                
                waypointPatrol.followPlayer = true;
                GameManager.Instance.estaDetectando = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sphereCastRadius);
    }
}
