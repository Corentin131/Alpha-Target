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
    public int staticNumberOfRecharge;

    bool stopSlowMotion;

    void Update()
    {
        if(win == true){
            loose = false;
        }
    }

    public IEnumerator Shake(float duration , float magnitudeX,float magnitudeY, GameObject objectToMove,bool canStop)
    {
        Vector2 originalPos = objectToMove.transform.localPosition;

        float elapsed = 0.0f;
        
        while(elapsed < duration)
        {
            float x  = Random.Range(-1,1) * magnitudeX;
            float y = Random.Range(-1,1) * magnitudeY;

            objectToMove.transform.localPosition = new Vector2(x,y);

            elapsed += Time.deltaTime;

            if(stopSlowMotion == true)
            {
                stopSlowMotion = false;
                break;
            }
            yield return null;
        }

        objectToMove.transform.localPosition = originalPos;
    }

    public void StopShake(){
        stopSlowMotion = true;
    }

    public IEnumerator SlowDawn(float timeScale,float delay)
    {
       
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = Time.timeScale * .02f;

        yield return new WaitForSeconds(delay* timeScale);

        while(timeScale < 1)
        {
            Time.timeScale = timeScale;
            Time.fixedDeltaTime = Time.timeScale * .02f;
            timeScale += Time.unscaledDeltaTime;            
            yield return null;
        }
        Time.timeScale = 1;
    }

    public Transform RecursiveFindChild(Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
            {
                return child;
            }
            else
            {
                Transform found = RecursiveFindChild(child, childName);
                if (found != null)
                {
                    return found;
                }
            }
        }
        return null;
    }

}
