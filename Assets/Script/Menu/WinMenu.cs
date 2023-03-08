using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public GlobalInforationscript gLS;
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
        if(gLS.win == true)
        {
            winMenu.SetActive(true);
            SceneManager.LoadScene("Level 2");
            //StartCoroutine(ChangeLevel(1.5f));
        }else if(gLS.loose == true)
        {
            gameOverMenu.SetActive(true);
        }
    }

    IEnumerator ChangeLevel(float second)
    {
        yield return new WaitForSeconds(second);
        SceneManager.LoadScene(levelToLoad);
    }
}
