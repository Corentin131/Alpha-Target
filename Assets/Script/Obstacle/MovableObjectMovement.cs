using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObjectMovement : MonoBehaviour
{
    public UnityEngine.Transform[] checkPoint;
    public float speed;
    UnityEngine.Transform nextPos;
    int posIndex;
    // Start is called before the first frame update
    void Start()
    {
        nextPos = checkPoint[0];
    }

    // Update is called once per frame
    void Update()
    {
        MoveObject();
    }

    void MoveObject(){
        if(transform.position == nextPos.position){
            posIndex ++;
            if(posIndex > checkPoint.Length){
                posIndex = 0;      
            }
        }
        nextPos = checkPoint[posIndex];
        transform.position = Vector2.MoveTowards(transform.position, nextPos.position, speed * Time.deltaTime);
        
    }
}
