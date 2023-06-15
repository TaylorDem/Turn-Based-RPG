using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    private Transform lookAt;    //need to check out what this is but its set to the player's transform object.
    public float boundX = 0.3f;
    public float boundY = 0.15f;

    private void Start()
    {
        lookAt = GameObject.Find("Player").transform;
    }

    private void LateUpdate()   //do this after we've ran all other update calls
    {
        Vector3 delta = Vector3.zero;   // make a (0,0,0) vector

        //Check if in bounds on X axis.
        float deltaX = lookAt.position.x - transform.position.x;    //get difference between player position (lookat) and camera's position (transform)
        //if difference is greater than my bounds
        if(deltaX > boundX || deltaX < -boundX) 
        {
            //if camera is further to left
            if(transform.position.x < lookAt.position.x)
            {  
                //set amount I need to move the camera positive
                delta.x = deltaX - boundX;
            }
            //if camera is further to right
            else
            {   
                //set amount I need to move negative
                delta.x = deltaX + boundX;
            }
        }

        //Check if in bounds on Y axis
        float deltaY = lookAt.position.y - transform.position.y;
        if(deltaY > boundY || deltaY < -boundY)
        {
            if(transform.position.y < lookAt.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }

        //actually move camera
        transform.position += new Vector3(delta.x,delta.y,0); //adds the vector onto the already existing one
    }
}
