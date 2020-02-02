using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignSpawner : MonoBehaviour
{
    public float timeBetweenWaves = 5f;
    public Vector3 spawnLocation = new Vector3(0.1f, -0.07f, 50f);
    public GameObject sign;
    private float timer = 10f;
    public float killLocation = -10f;
    public void spawnASign()
    {
        Instantiate(sign, spawnLocation, Quaternion.identity);
    }

    public void Update()
    {
        timer -= Time.deltaTime;
        if (timeBetweenWaves != 0 && timer <= 0)
        {
                timer = timeBetweenWaves;
                spawnASign();
                Debug.Log("timer went off");
        }
    }

    public void FixedUpdate()
    {
        if (sign.transform.position.z <= killLocation)
        {
            Destroy(sign);
        }
    }
}