using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float rotationSpeed;
    public float speedPearSecond;
    public bool increasingSpeed;

    float runningRotationSpeed;
    void  Start()
    {
        if(increasingSpeed == true){
            runningRotationSpeed = 0;
        }else{
            runningRotationSpeed = rotationSpeed;
        }
    }
    // Update is called once per frame
    void  FixedUpdate()
    {
        if(increasingSpeed == true){

            runningRotationSpeed = runningRotationSpeed + speedPearSecond;

            if (runningRotationSpeed > rotationSpeed || runningRotationSpeed < -rotationSpeed)
            {
                speedPearSecond = -speedPearSecond;
            }
        }
    }
    void Update()
    {   
        gameObject.transform.Rotate(0f,0f,runningRotationSpeed*Time.deltaTime, Space.Self);
    }
    
    
}
