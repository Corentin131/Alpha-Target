using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMenu : MonoBehaviour
{
    public GlobalInforationscript gLS;
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
            winMenu.SetActive(true);
        }else if(gLS.loose == true){
            gameOverMenu.SetActive(true);
        }
    }
}
