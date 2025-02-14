using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuletBehaviour : MonoBehaviour
{
    float timer = 5f;
    float speed = 2f;
    public GenericPool bulletPool;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = 5f;
            bulletPool.ReturnToPool(gameObject);
        }
        if (Input.GetMouseButtonDown(0))
        {
            bulletPool.GetElementFromPool();
        }
    }
}
