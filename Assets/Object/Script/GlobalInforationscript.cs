using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInforationscript : MonoBehaviour
{
    public bool canShot = false;
    public bool Recharge = false;
    public bool win = false;
    public bool loose = false;
    public float timerRecharge;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(win == true){
            loose = false;
        }
    }
}
