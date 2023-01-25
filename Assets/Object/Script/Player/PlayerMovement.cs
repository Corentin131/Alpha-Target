using System.Threading;
using System.Diagnostics;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject playerRotation;
    public GameObject projectile;
    public float percentageOfLimit;
    public int numberOfRecharge;
    public bool canShot = true;
    public bool canRecharge = false;
    public float staticTimerFire;
    public float timeBeforeAnimationShooterRecharge;
    public string nameTriggerAnimatorShooterRecharge;
    public string nameTriggerAnimatorShooterShoot;
    public Animator animatorShooter;
    public string nameTriggerAnimatorCameraAnimator;
    public Animator animatorCamera;
    public GameObject numberOfRechargeTextGameObject;
    public GameObject sliderRechargeFilGameObject;
    //public Slider sliderRecharge;
    TextMeshProUGUI numberOfRechargeText;
    Animator numberOfRechargeTextAnimator;
    GlobalInforationscript gLS;
    //Animator sliderRechargeFilAnimator;
    int runningNumberOfRecharge;
    float runningTimerFire;
    // Start is called before the first frame update
    void Start()
    {
        gLS = GameObject.Find("GlobalInformation").GetComponent<GlobalInforationscript>();
        numberOfRechargeText = numberOfRechargeTextGameObject.GetComponent<TextMeshProUGUI>();
        numberOfRechargeTextAnimator = numberOfRechargeTextGameObject.GetComponent<Animator>();

        gLS.timerRecharge = staticTimerFire;
        runningNumberOfRecharge = numberOfRecharge;
        numberOfRechargeText.text =  runningNumberOfRecharge.ToString()+"/"+numberOfRecharge.ToString();

        runningTimerFire = staticTimerFire;
    }

    // Update is called once per frame
    void Update()
    {   
        if(canRecharge == false && canShot == false && gLS.loose == false && gLS.win == false){
            runningTimerFire -= Time.deltaTime;
            gLS.timerRecharge  = staticTimerFire-runningTimerFire;
        }
        if(runningTimerFire < 0){
            canShot = true;
            canRecharge = false;
            runningTimerFire = staticTimerFire;
        }
        if (Input.touchCount > 0 && gLS.win == false && gLS.loose == false){
            Touch touch = Input.GetTouch(0);

            if (canShot == false && canRecharge == true){
                Recharge();
            }
            if(touch.phase == TouchPhase.Ended && canShot == true && canRecharge == false){
                Shoot();
            }
            Movement(touch);
        }
        if (runningNumberOfRecharge <= 0){
           StartCoroutine(GameOver());
        }
    }   

    void Movement(Touch touch){
        float angle = ((360/100)*((touch.position.x/Screen.width)*percentageOfLimit)); //playerRotation.transform.eulerAngles.z;
        float angleResult = angle* Time.deltaTime;
        playerRotation.transform.localEulerAngles= new Vector3(0f,0f,angle);
    }
    void Shoot(){
        animatorShooter.SetTrigger(nameTriggerAnimatorShooterShoot);

        runningNumberOfRecharge -= 1;
        numberOfRechargeText.text = runningNumberOfRecharge.ToString()+"/"+numberOfRecharge.ToString();
        numberOfRechargeTextAnimator.SetTrigger("Loose");

        if(nameTriggerAnimatorCameraAnimator != ""){
            animatorCamera.SetTrigger(nameTriggerAnimatorCameraAnimator);
        }
        
        gLS.canShot = true;
        canShot = false;
        canRecharge = true;
    }

    void Recharge(){
        Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);

        StartCoroutine(CoolDownBeforeAnimator(timeBeforeAnimationShooterRecharge,animatorShooter,nameTriggerAnimatorShooterRecharge));

        canShot = false;
        canRecharge = false;
    }

    IEnumerator CoolDownBeforeAnimator(float secondBeforeAnimator, Animator animatorToSetTrigger,string trigger) {
        yield return new WaitForSecondsRealtime(secondBeforeAnimator);
        animatorToSetTrigger.SetTrigger(trigger);
    }

    IEnumerator GameOver(){
        yield return new WaitForSecondsRealtime(1);
        gLS.loose = true;
        canShot = false;
        canRecharge = false;
    }

}
