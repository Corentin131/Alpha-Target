using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public GameObject winMenu;
    public GameObject gameOverMenu;
    public int levelToLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(BrainGame.win == true)
        {
            winMenu.SetActive(true);

        }else if(BrainGame.loose == true)
        {
            gameOverMenu.SetActive(true);
        }
    }

}
