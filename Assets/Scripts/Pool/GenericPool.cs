using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;
    Stack<GameObject> _pool = new Stack<GameObject>();
    [SerializeField]
    int poolSize;
    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            CreateElement();
        }
    }
    GameObject CreateElement()
    {
        GameObject temporalElement = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
        temporalElement.GetComponent<Bullet>().pool = this;
        _pool.Push(temporalElement);
        temporalElement.SetActive(false);
        return temporalElement;
    }

    public GameObject GetElementFromPool()
    {
        GameObject toReturn = null;
        if (_pool.Count == 0)
        {
            toReturn = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
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
