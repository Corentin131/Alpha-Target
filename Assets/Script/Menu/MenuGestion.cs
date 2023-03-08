using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGestion : MonoBehaviour
{
    public GlobalInforationscript gLS;
    public GameObject winMenu;
    public GameObject gameOverMenu;
    public int levelToLoad;

    bool loadScene = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gLS.win == true){
            StartCoroutine(ActiveMenu(winMenu));
        }else if(gLS.loose == true){
            StartCoroutine(ActiveMenu(gameOverMenu));
        }
        if(loadScene == true)
        {
            SceneManager.LoadScene(levelToLoad);
        }
        
    }
    IEnumerator ActiveMenu(GameObject menu)
    {
        yield return new WaitForSecondsRealtime(1);
        menu.SetActive(true);
        yield return new WaitForSecondsRealtime(4f);
        loadScene = true;
    }
}
