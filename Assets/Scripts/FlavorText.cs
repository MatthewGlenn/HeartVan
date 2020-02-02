using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FlavorText : MonoBehaviour
{
    public SceneHandler sceneHandler;
    public float timeToNext;
    public GameObject step1;
    public GameObject step2;
    public GameObject step3;
    public GameObject step4;
    public GameObject step5;
    public GameObject step6;
    public GameObject step7;
    public GameObject step8;
    public AudioManager am;

    float timer;
    int step;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        step = 1;
        am = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= timeToNext)
        {
            timer = 0;
            step++;
        }

        if(step == 2)
        {
            step1.SetActive(false);
            step2.SetActive(true);
        }

        if(step == 3)
        {
            step2.SetActive(false);
            step3.SetActive(true);
        }

        if (step == 4)
        {
            step3.SetActive(false);
            step4.SetActive(true);
        }

        if (step == 5)
        {
            step4.SetActive(false);
            step5.SetActive(true);
        }

        if (step == 6)
        {
            step5.SetActive(false);
            step6.SetActive(true);
        }

        if (step == 7)
        {
            step6.SetActive(false);
            step7.SetActive(true);
        }

        if (step == 8)
        {
            step7.SetActive(false);
            step8.SetActive(true);
        }

        if(step == 9)
        {
            sceneHandler.LoadNextScene();
            am.StartGame();
        }
    }
}
