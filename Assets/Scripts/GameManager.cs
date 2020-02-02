using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	// Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject signPrefab;
    public float signZstart = 100f;

    private float deltaTime = 0f;

    SignController sc;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate at position (0, 0, 0) and zero rotation.
        GameObject sign = Instantiate(signPrefab, new Vector3(0, 0, signZstart), Quaternion.identity);
        sc = sign.GetComponent<SignController>();
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += Time.deltaTime;
        if (deltaTime > 7f) {
        	sc.next();
        	deltaTime = 0f;
        }
    }
}
