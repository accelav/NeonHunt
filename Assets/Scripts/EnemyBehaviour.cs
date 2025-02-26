using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    int puntosAlMorir = 2;

    WaypointPatrol waypointPatrol;
    
    void Start()
    {
        
        GameManager.Instance.EnemiesCounter(1);
        waypointPatrol = GetComponent<WaypointPatrol>();
    }

    // Update is called once per frame
    void Update()
    {

    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Bullet")
        {
            SoundsBehaviour.instance.PlayExplosionTwo();
            GameManager.Instance.OtorgarPuntos(puntosAlMorir);
            //GameManager.Instance.EnemiesCounter(-1);
            Destroy(gameObject);
        }

    }

    private void OnDestroy()
    {
        GameManager.Instance.EnemiesCounter(-1);
    }
}
