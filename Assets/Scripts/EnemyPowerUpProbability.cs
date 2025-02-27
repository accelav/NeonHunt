using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPowerUpProbability : MonoBehaviour
{
    [SerializeField]
    GameObject[] powerUpPrefab;
    int probabilidad;
    [SerializeField ]int probabilidadUno = 10;
    [SerializeField] int probabilidadDos = 20;
    List<int> prefabProb = new List<int>();
    List<int> prefabProbDos = new List<int>();
    bool esProbable = false;


    public void InstantiateGameObject()
    {
        Probabilidad();

        if (esProbable)
        {
            
        }
        
    }
    void Probabilidad()
    {
        probabilidad = Random.Range(0, 100);

        for (int i = 0; i < probabilidadUno; i++)
        {
            prefabProb.Add(i);
            if (probabilidad == i)
            {
                Debug.Log("Power Up Uno");
                Vector3 posicion = gameObject.transform.position;
                GameObject prefab = Instantiate(powerUpPrefab[0], posicion, Quaternion.identity);
                //esProbable = true;
            }
        }

        for (int i = 15; i < probabilidadDos; i++)
        {
            prefabProbDos.Add(i);
            if (probabilidad == i)
            {
                Debug.Log("Power Up Dos");
                Vector3 posicion = gameObject.transform.position;
                GameObject prefab = Instantiate(powerUpPrefab[1], posicion, Quaternion.identity);
                //esProbable = true;
            }
        }
    }
}
