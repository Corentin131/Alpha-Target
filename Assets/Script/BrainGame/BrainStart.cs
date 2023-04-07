using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainStart : MonoBehaviour
{
    public  GameObject canvas;
    public  GameObject cameraHolder;
    GameObject coinUI;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 100;
        coinUI = GlobalFunctions.RecursiveFindChild(canvas.transform,"Coin").gameObject;
        BrainGame.Initialize(coinUI,canvas,cameraHolder);
    }
}
