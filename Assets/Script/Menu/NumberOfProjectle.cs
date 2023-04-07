using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberOfProjectle : MonoBehaviour
{
    public GameObject iconExample;
    public float distance;

    GameObject[] icons;
    int numberOfRecharge;

    void Update() 
    {
        if (numberOfRecharge != BrainGame.numberOfRecharge)
        {
            numberOfRecharge = BrainGame.numberOfRecharge;
            StartCoroutine(Display());
        }
    }

    IEnumerator Display()
    {   
        Debug.Log(numberOfRecharge);
        
        Vector2 position = transform.position;

        foreach (int value in Enumerable.Range(0, numberOfRecharge))
        {
            GameObject icon = Instantiate(iconExample,position,iconExample.transform.rotation);
            icon.transform.parent = transform;
            position.x = position.x-distance;

            yield return null;
        }
        
    }
    /*
    TextMeshProUGUI text;
    float minimum;
    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text =  BrainGame.runningNumberOfRecharge.ToString();

        if(BrainGame.runningNumberOfRecharge == 1){
            StartCoroutine(GlobalFunctions.Shake(0.1f,3f,3f,gameObject,true));
            text.color = ColorFromBytes(245,4,0);
        }
    }

    public static Color ColorFromBytes(byte r, byte g, byte b, byte a = 255)
    {
            return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
    }
    */
}
