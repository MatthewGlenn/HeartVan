using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignFactory : MonoBehaviour
{
	// Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject signPrefab;
    public float signZstart = 100f;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate at position (0, 0, 0) and zero rotation.
        GameObject sign = Instantiate(signPrefab, new Vector3(0, 0, signZstart), Quaternion.identity);
        SignController sc = sign.GetComponent<SignController>();
        sc.setLeft();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
