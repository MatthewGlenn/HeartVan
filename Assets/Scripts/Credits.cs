using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public SceneHandler sceneHandler;
    public float timeToMainMenu;

    float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = timeToMainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            sceneHandler.ReturnToMainMenu();
        }
    }
}
