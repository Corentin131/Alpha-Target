using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RechargeBar : MonoBehaviour
{
    public Slider sliderRecharge;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        sliderRecharge.value = BrainGame.timerRecharge;
    }
}
