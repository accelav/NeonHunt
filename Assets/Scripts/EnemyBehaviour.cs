using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    int puntosAlMorir = 2;
    bool haChocado;
    void Start()
    {
        haChocado = false;
        GameManager.Instance.EnemiesCounter(1);
    }

    // Update is called once per frame
    void Update()
    {

    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Bullet")
        {

            GameManager.Instance.OtorgarPuntos(puntosAlMorir);
            GameManager.Instance.EnemiesCounter(-1);
            Destroy(gameObject);
        }
    }

}
