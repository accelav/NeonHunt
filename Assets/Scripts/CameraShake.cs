using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    CinemachineCameraOffset cameraOffset;
    bool isShaking;
    public float duracion = 1f;
    float timer = 0f;
    public float velocidad = 1f;
    public float distancia = 2f;
    void Start()
    {
        cameraOffset = GetComponent<CinemachineCameraOffset>();
        timer = duracion;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if(!isShaking)
        {

        }
        else
        {
            timer -= Time.deltaTime;
            float t = Mathf.PingPong(Time.time * velocidad, distancia);
            cameraOffset.m_Offset = new Vector3(0, 0, t);
            if (timer <= 0f)
            {
                timer = duracion;
                isShaking = false;
                cameraOffset.m_Offset = new Vector3(0, 0, 0);

            }
        }
    }

    public void Shake()
    {
        isShaking = true;
        
    }
}
