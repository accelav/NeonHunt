using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    int puntosAlMorir = 2;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        if (damage == 1)
        {
            Debug.Log("Enemigo Destrudo");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Bullet")
        {
            GameManager.Instance.OtorgarPuntos(puntosAlMorir);
            Destroy(gameObject);
        }
    }
}
