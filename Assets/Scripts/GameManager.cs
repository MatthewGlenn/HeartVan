using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	// Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject sign;
    public Vector3 signSpawnPosition;

    private float deltaTime = 0f;

    private bool started = false;

    SignController sc;

    // Start is called before the first frame update
    void Start()
    {
        sc = sign.GetComponent<SignController>();
        Debug.Log(sc);
    }

    // Update is called once per frame
    void Update()
    {
    	if (started) {
            Debug.Log(deltaTime);
	        deltaTime += Time.deltaTime;
	        if (deltaTime > 7f) {
	        	sc.next();
	        	deltaTime = 0f;
        	}
    	}
    }

    public void startGame()
    {
        Debug.Log("hit startGame");
    	started = true;
    }
}
