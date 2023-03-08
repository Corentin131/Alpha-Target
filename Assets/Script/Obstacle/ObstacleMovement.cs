using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float RotationSpeed;
    public float speedPearSecond = 2;
    public bool increasingSpeed;
    float runningRotationSpeed;
    // Start is called before the first frame update
    void  Start()
    {
        if(increasingSpeed == true){
            runningRotationSpeed = 0;
        }else{
            runningRotationSpeed = RotationSpeed;
        }
    }
    // Update is called once per frame
    void Update()
    {   
        if(increasingSpeed == true){
            runningRotationSpeed += speedPearSecond * Time.deltaTime;
            if (runningRotationSpeed > RotationSpeed || runningRotationSpeed < -RotationSpeed){
                speedPearSecond = -speedPearSecond;
            }
        }
        gameObject.transform.Rotate(0f,0f,runningRotationSpeed * Time.deltaTime, Space.Self);
    }

}
