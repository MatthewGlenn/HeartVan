using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody rigidbody;
    void FixedUpdate()
    {
        rigidbody.AddForce(0, 0, -speed * Time.fixedDeltaTime);
    }
}
