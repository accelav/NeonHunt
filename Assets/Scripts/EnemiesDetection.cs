using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField]
    float sphereCastRadius = 10f;
    [SerializeField]
    float maxRayDistance = 0;
    private void Update()
    {
        RaycastHit[] hits = Physics.SphereCastAll(
            transform.position,
            sphereCastRadius,
            transform.forward,
            maxRayDistance);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player Detectado");
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sphereCastRadius);
    }
}
