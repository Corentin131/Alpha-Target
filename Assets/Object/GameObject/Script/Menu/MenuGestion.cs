using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGestion : MonoBehaviour
{public GlobalInforationscript gLS;
    public GameObject winMenu;
    public GameObject gameOverMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gLS.win == true){
            StartCoroutine(activeMenu(winMenu));
        }else if(gLS.loose == true){
            StartCoroutine(activeMenu(gameOverMenu));
        }
    }
    IEnumerator activeMenu(GameObject menu){
        yield return new WaitForSecondsRealtime(1);
        menu.SetActive(true);
    }
}
