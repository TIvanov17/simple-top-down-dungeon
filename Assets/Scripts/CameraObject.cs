using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObject : MonoBehaviour
{
    public Transform lookAt;
    
    public float xBound = 0.15f;
    public float yBound = 0.05f;

    private void Start()
    {
        lookAt = GameObject.Find("Player").transform;
    }
    private void LateUpdate()
    {
       
        float deltaX = lookAt.position.x - transform.position.x;
        float deltaY = lookAt.position.y - transform.position.y;

        Vector3 delta = CheckBoundAndMove(Vector3.zero, deltaX, deltaY);

        transform.position += new Vector3(delta.x, delta.y, 0);
    }

    private Vector3 CheckBoundAndMove(Vector3 delta, float deltaX, float deltaY)
    {
        // is we are inside the x - BOUND 
        if (IsOutBound(deltaX, xBound))
        {
            delta.x = transform.position.x < lookAt.position.x ?
                      deltaX - xBound :
                      deltaX + xBound;

        }

        // is we are inside the y - BOUND 
        if (IsOutBound(deltaY, yBound))
        {
            delta.y = transform.position.y <  lookAt.position.y ?
                      deltaY - yBound :
                      deltaY + yBound;

        }

        return delta;
    }
    private bool IsOutBound(float coordinate, float bound)
    {
        return coordinate > bound || coordinate < -bound;
    }
}