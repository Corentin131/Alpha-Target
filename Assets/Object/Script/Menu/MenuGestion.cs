using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGestion : MonoBehaviour
{
    public GameObject winnerMenu;
    public GameObject looseMenu;
    public float[] timeDelayBeforePlayAnimationWinMenu; 
    public Animator[] enterAnimatorWinMenu;

    public float[] timeDelayBeforePlayAnimationLooseMenu; 
    public Animator[] enterAnimatorLooseMenu;

    public GlobalInforationscript gLS;

    bool menuActive;
    // Start is called before the first frame update
    void Start()
    {
        winnerMenu.SetActive(false);//Il faut faire sa sinon il y a un peiti freez quand on touch la cible
        looseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(gLS.win == true && menuActive == false){
            //Debug.Log("pj");
            PlayAnimator(timeDelayBeforePlayAnimationWinMenu,enterAnimatorWinMenu,winnerMenu);
        }else if(gLS.loose == true && menuActive == false && gLS.win == false){
            
            PlayAnimator(timeDelayBeforePlayAnimationLooseMenu,enterAnimatorLooseMenu,looseMenu);
        }

    }
    IEnumerator PlayAnimation(float time , string triggerName,Animator enterAnimators){
        yield return new WaitForSecondsRealtime(time);
        enterAnimators.SetTrigger("Enter");
    }

    void PlayAnimator(float[] timeDelayBeforePlayAnimation,Animator[] enterAnimator,GameObject Menu){
        int index = 0;
        Menu.SetActive(true);
        foreach(Animator enterAnimators in enterAnimator){
            StartCoroutine(PlayAnimation(timeDelayBeforePlayAnimation[index],"Enter",enterAnimators));
            index ++;
        }
        menuActive = true;
        index ++;
    }
}
