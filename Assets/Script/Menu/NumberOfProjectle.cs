using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberOfProjectle : MonoBehaviour
{
    public GlobalInforationscript gLS;

    TextMeshProUGUI text;
    float minimum;
    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text =  gLS.runningNumberOfRecharge.ToString();

        if(gLS.staticNumberOfRecharge/3*3 >= gLS.runningNumberOfRecharge){
            text.color = ColorFromBytes(3,181,19);
        }
        if(gLS.staticNumberOfRecharge/3*2 >= gLS.runningNumberOfRecharge){
            text.color = ColorFromBytes(246,190,0);
        }
        if(gLS.staticNumberOfRecharge/3*1 >= gLS.runningNumberOfRecharge){
            StartCoroutine(gLS.Shake(0.1f,3f,3f,gameObject,true));
            text.color = ColorFromBytes(245,4,0);
        }
    }

    public static Color ColorFromBytes(byte r, byte g, byte b, byte a = 255)
    {
            return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
    }
}
