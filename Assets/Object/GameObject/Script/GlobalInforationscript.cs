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
    public int runningNumberOfRecharge;

    void Update()
    {
        if(win == true){
            loose = false;
        }
    }

    public IEnumerator Shake(float duration , float magnitudeX,float magnitudeY, GameObject objectToMove)
    {
        Vector2 originalPos = objectToMove.transform.localPosition;

        float elapsed = 0.0f;
        
        while(elapsed < duration){
            float x  = Random.Range(-1,1) * magnitudeX;
            float y = Random.Range(-1,1) * magnitudeY;

            objectToMove.transform.localPosition = new Vector2(x,y);

            elapsed += Time.deltaTime;

            yield return null;
        }

        objectToMove.transform.localPosition = originalPos;
    }
}
