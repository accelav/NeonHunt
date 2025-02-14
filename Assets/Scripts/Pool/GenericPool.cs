using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPool : MonoBehaviour
{
    Stack<GameObject> _pool = new Stack<GameObject>();

    [SerializeField]
    GameObject prefabToInstantiate;

    [SerializeField]
    int poolSize;

    [SerializeField]
    GameObject cañones;
    void Start()
    {
        for ( int i = 0; i < poolSize; i++)
        {
            CreateElement();
        }
    }
    GameObject CreateElement()
    {
        GameObject temporalElement = Instantiate(prefabToInstantiate, Vector3.zero, Quaternion.identity);
        temporalElement.GetComponent<BuletBehaviour>().bulletPool = this;
        _pool.Push(temporalElement);
        temporalElement.SetActive(false);
        return temporalElement;
    }

    public GameObject GetElementFromPool()
    {
        GameObject toReturn = null;
        if (_pool.Count == 0)
        {
            toReturn = Instantiate(prefabToInstantiate, cañones.transform.position, Quaternion.identity);
            toReturn.SetActive(false);
        }
        else
        {
            toReturn = _pool.Pop();
        }
        return toReturn;
    }

    public void ReturnToPool(GameObject element)
    {
        element.SetActive(false);
        _pool.Push(element);
    }
}
