using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	// Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject sign;
    public GameObject van;
    public AudioManager audioManager;

    private float deltaTime = 0f;

    private bool started = false;

    SignController sc;

    // Start is called before the first frame update
    void Start()
    {
        sc = sign.GetComponent<SignController>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
    	if (started) {
	        deltaTime += Time.deltaTime;
	        if (deltaTime > 7f) {
                //Check the gates
                
	        	started = false;
        	}
    	}
    }

    public void resetLoop()
    {
        sc.next();
        deltaTime = 0f;
        started = true;
    }

    public void startGame()
    {
    	started = true;
    }
}
