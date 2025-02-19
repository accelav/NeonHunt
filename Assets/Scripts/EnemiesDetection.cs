using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField]
    float sphereCastRadius = 10f;
    [SerializeField]
    float maxRayDistance = 0;
    WaypointPatrol waypointPatrol;
    PlayerController playerController;
    
    private void Start()
    {
        waypointPatrol = GetComponent<WaypointPatrol>();
        playerController = FindAnyObjectByType<PlayerController>();
    }
    private void Update()
    {

        RaycastHit[] hits = Physics.SphereCastAll(
            transform.position,
            sphereCastRadius,
            transform.forward,
            maxRayDistance);
        waypointPatrol.followPlayer = false;
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player Detectado");
                waypointPatrol.followPlayer = true;
            }
            
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sphereCastRadius);
    }
}
