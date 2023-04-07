using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalFunctions
{
    static bool stopSlowMotion;
    public static IEnumerator Shake(float duration , float magnitudeX,float magnitudeY, GameObject objectToMove,bool canStop)
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

    public static void StopShake(){
        stopSlowMotion = true;
    }

    public static IEnumerator DestroyDelay(GameObject gameObject ,float delay)
    {
        yield return new WaitForSeconds(delay);
        Object.Destroy(gameObject);
    }

   public static void SpawnEffect(GameObject[] spawnEffects,Transform spawnEffectTransform)
   {
        foreach(GameObject objectToSpawn in spawnEffects)
        {
            if(objectToSpawn.tag == "Text")
            {
                spawnEffectTransform.eulerAngles = new Vector3(0,0,0);
            }
            Object.Instantiate(objectToSpawn, spawnEffectTransform.position,spawnEffectTransform.rotation);
        }
   }

   public static Transform RecursiveFindChild(Transform parent, string childName)
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

    public static IEnumerator SlowDawn(float timeScale,float delay)
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
}
