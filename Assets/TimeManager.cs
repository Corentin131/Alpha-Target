using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(SlowDawn(0.05f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SlowDawn(float timeScale)
    {
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = Time.timeScale * .02f;

        yield return new WaitForSeconds(20 * timeScale);

        while(timeScale < 1)
        {
            Time.timeScale = timeScale;
            Time.fixedDeltaTime = Time.timeScale * .02f;
            timeScale += Time.deltaTime;            
            yield return null;
        }
        Time.timeScale = 1;
    }
}
