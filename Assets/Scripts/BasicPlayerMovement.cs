using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float force = 200f;
    public float sidewaysForce = 500f;
    private string input;
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(0, 0, force * Time.deltaTime);
        int direction = 1;
        if ( Input.GetKey("d")){
            direction = 1;
        }else if ( Input.GetKey("a")){
            direction = -1;
        }else{
            direction = 0;
        }
        rb.AddForce(direction * sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
    }
}
