using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class Text : MonoBehaviour
{

    public float delayBeforeDestroy;
    public List<string> randomWord = new List<string>();
    
    Vector2 randomVector2;
    TextMeshPro text;
    void Start()
    {   
       
        text = gameObject.GetComponent<TextMeshPro>();
        var random = new System.Random();
        int index = random.Next(randomWord.Count);
        text.text = randomWord[index];
        gameObject.transform.eulerAngles = new Vector3(0,0,0);
      
    }
    void Update()
    {   
       
        transform.Translate(new Vector2(0*Time.deltaTime, 1*Time.deltaTime));
            
        
        StartCoroutine(Destroy(delayBeforeDestroy));
    }

    IEnumerator Destroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
