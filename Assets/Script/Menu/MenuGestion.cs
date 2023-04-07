using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGestion : MonoBehaviour
{
    public GameObject winMenu;
    public GameObject gameOverMenu;
    public int levelToLoad;
    public float secondToLoadScene;
    public float secondToActiveWinMenu;
    bool loadScene = false;

    // Update is called once per frame
    void Update()
    {
        if(BrainGame.win == true){
            StartCoroutine(ActiveMenu(winMenu,secondToLoadScene,secondToActiveWinMenu));
        }else if(BrainGame.loose == true){
            StartCoroutine(ActiveMenu(gameOverMenu,secondToLoadScene,secondToActiveWinMenu));
        }
        if(loadScene == true)
        {
            SceneManager.LoadScene(levelToLoad);
        }
        
    }
    IEnumerator ActiveMenu(GameObject menu,float secondToLoadScene,float secondToActiveWinMenu)
    {
        yield return new WaitForSecondsRealtime(secondToActiveWinMenu);
        menu.SetActive(true);
        yield return new WaitForSecondsRealtime(secondToLoadScene);
        loadScene = true;
    }
}
