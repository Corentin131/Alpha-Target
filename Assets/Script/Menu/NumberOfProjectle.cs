using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberOfProjectle : MonoBehaviour
{
    public GameObject iconExample;
    public float distance;

    List<GameObject> icons = new List<GameObject>();
    int numberOfRecharge = 0;

    void Update() 
    {
        if (numberOfRecharge < BrainGame.numberOfRecharge)
        {
            numberOfRecharge = BrainGame.numberOfRecharge;
            Display();
        }
        
        if(numberOfRecharge > BrainGame.numberOfRecharge)
        {
            Remove();
            numberOfRecharge = BrainGame.numberOfRecharge;
        }
    }

    void Display()
    {   
        icons.Clear();

        Vector2 position = transform.position;

        foreach (int value in Enumerable.Range(0, numberOfRecharge))
        {
            GameObject icon = Instantiate(iconExample,position,iconExample.transform.rotation);
            icon.transform.parent = transform;
            position.x = position.x-distance;
            icons.Add(icon);
        }
    }
    void Remove()
    {
        foreach (int value in Enumerable.Range(0, numberOfRecharge-BrainGame.numberOfRecharge))
        {
            GameObject icon = icons.Last();
            icons.Remove(icons.Last());
            Destroy(icon);
        }
    }
}
