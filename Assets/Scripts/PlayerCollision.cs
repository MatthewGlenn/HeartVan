using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public BasicPlayerMovement movement;
    void OnCollisionEnter(Collision collisionInfo){
        if (collisionInfo.collider.tag == "Obstacle") {
            Debug.Log("We hit an obstacle");
            movement.enabled = false;
        }
    }
}
