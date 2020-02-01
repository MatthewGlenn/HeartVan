using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float force = 200f;
    public float sidewaysForce = 500f;
    public Transform road;
    public float timeToDriftToCenter = 4f;
    private enum Lanes {
        Left = -2,
        Middle = 0,
        Right = 2
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float movement = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * sidewaysForce;
        
        if (Input.GetKey("a")){
            moveToLane(Lanes.Left);
        }else if (Input.GetKey("w")){
            moveToLane(Lanes.Middle);
        }else if (Input.GetKey("d")){
            moveToLane(Lanes.Right);
        }
        
    }

    //
    Lanes getLaneForInput(string input)
    {
        //TODO: Set the Correct Lane best on the input
        return Lanes.Middle;
    }
    
    void moveToLane(Lanes lane){
        //Debug.Log(lane);
        
        dummyMovement();
        //TODO:Replace Dummy with animation controller calls in the switch statement
        switch (lane)
        {
            case Lanes.Left:
                Debug.Log("Move "+lane);
                break;
            case Lanes.Middle:
                Debug.Log("Move "+lane);
                break;
            case Lanes.Right:
                Debug.Log("Move "+lane);
                break;
        }
        driftToCenter();
    }

    //Drift back to the center after making some movement
    private void driftToCenter(){

    }

    //TODO: Replace me with animation controller alternative
    private void dummyMovement()
    {
        float movement = Input.GetAxis(("Horizontal")) * Time.fixedDeltaTime * sidewaysForce;
        Vector2 newPosition = rb.position + Vector3.right * movement;
        float mapWidth = 1.5f;
        newPosition.x = Mathf.Clamp(newPosition.x, -mapWidth, mapWidth);
        rb.MovePosition(newPosition);
    }
}
