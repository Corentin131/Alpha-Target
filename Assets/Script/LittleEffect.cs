using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class LittleEffect : MonoBehaviour
{
    public string type;
    public float delayBeforeDestroy;
    public List<string> randomWord = new List<string>();
    
    Vector2 randomVector2;
    TextMeshPro text;
    void Start()
    {   
        switch(type){
            case "Text movement":
                text = gameObject.GetComponent<TextMeshPro>();
                var random = new System.Random();
                int index = random.Next(randomWord.Count);
                text.text = randomWord[index];
            break;    
        }
    }
    void Update()
    {   
        switch(type){
            case "Text movement":
                transform.Translate(new Vector2(0*Time.deltaTime, 1*Time.deltaTime));
            break;
        }
        StartCoroutine(Destroy(delayBeforeDestroy));
    }

    IEnumerator Destroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
