using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinFade : MonoBehaviour
{
    public SceneHandler sceneHandler;

    private Image image;
    private float alpha = 0;

    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        alpha += Time.deltaTime;
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);

        if(alpha >= 2)
        {
            sceneHandler.LoadNextScene();
        }
    }
}
