
using UnityEngine;


public class EnemiesAttack : MonoBehaviour
{

    [SerializeField]
    float sphereCastRadius;
    [SerializeField]
    float maxRayDistance;

    private void Update()
    {

        RaycastHit[] hits = Physics.SphereCastAll(
            transform.position,
            sphereCastRadius,
            transform.forward,
            maxRayDistance
            
        );

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Player Muerto");
                GameManager.Instance.ReempezarPartida();
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        float maxDistance = maxRayDistance;
        Vector3 basePosition = transform.position;

        for (float j = 0; j < maxDistance; j++)
        {
            Vector3 spherePosition = basePosition + transform.forward * j;
            Gizmos.DrawWireSphere(spherePosition, sphereCastRadius);
        }
    }
}
