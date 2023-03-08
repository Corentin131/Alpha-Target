using System.Threading;
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
    //public string nameTriggerAnimatorCameraAnimator;
    public Animator animatorCamera;
    public GameObject cameraHolder;
    public GameObject numberOfRechargeTextGameObject;
    //public Slider sliderRecharge;
    TextMeshProUGUI numberOfRechargeText;
    Animator numberOfRechargeTextAnimator;
    GlobalInforationscript gLS;
    //Animator sliderRechargeFilAnimator;
    int runningNumberOfRecharge;
    float runningTimerFire;
    float minimum;

    // Start is called before the first frame update
    void Start()
    {
        
        gLS = GameObject.Find("GlobalInformation").GetComponent<GlobalInforationscript>();
        numberOfRechargeText = numberOfRechargeTextGameObject.GetComponent<TextMeshProUGUI>();
        numberOfRechargeTextAnimator = numberOfRechargeTextGameObject.GetComponent<Animator>();

        gLS.timerRecharge = staticTimerFire;
        runningNumberOfRecharge = numberOfRecharge;
        gLS.runningNumberOfRecharge = runningNumberOfRecharge;
        numberOfRechargeText.text =  runningNumberOfRecharge.ToString();//+"/"+numberOfRecharge.ToString();

        minimum = gLS.runningNumberOfRecharge/3;

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
        }else if(Input.touchCount <= 0 && canShot == true){
            Shoot();
        }
        if(runningNumberOfRecharge != gLS.runningNumberOfRecharge){
            numberOfRechargeText.text = gLS.runningNumberOfRecharge.ToString();//+"/"+numberOfRecharge.ToString();
            numberOfRechargeTextAnimator.SetTrigger("Loose");
        }
        runningNumberOfRecharge = gLS.runningNumberOfRecharge;
        //Debug.Log(minimum*2);
        //Debug.Log(gLS.runningNumberOfRecharge);
        if(minimum*3 >= gLS.runningNumberOfRecharge){
            numberOfRechargeText.color = ColorFromBytes(3,181,19);
        }
        if(minimum*2 >= gLS.runningNumberOfRecharge){
            StartCoroutine(gLS.Shake(0.1f,3f,3f,numberOfRechargeTextGameObject,true));
            numberOfRechargeText.color = ColorFromBytes(246,190,0);
        }
        if(minimum*1 >= gLS.runningNumberOfRecharge){
            StartCoroutine(gLS.Shake(0.1f,6f,6f,numberOfRechargeTextGameObject,true));
            numberOfRechargeText.color = ColorFromBytes(245,4,0);
        }
    }   

    public static Color ColorFromBytes(byte r, byte g, byte b, byte a = 255)
    {
            return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
    }
    void Movement(Touch touch){
        float angle = ((360/100)*((touch.position.x/Screen.width)*percentageOfLimit)); //playerRotation.transform.eulerAngles.z;
        float angleResult = angle* Time.deltaTime;
        playerRotation.transform.localEulerAngles= new Vector3(0f,0f,angle);
    }
    void Shoot(){
        animatorShooter.SetTrigger(nameTriggerAnimatorShooterShoot);
        //if(nameTriggerAnimatorCameraAnimator != ""){
        //animatorCamera.SetTrigger("Shoot");
        //}
        
        gLS.canShot = true;
        canShot = false;
        canRecharge = true;
    }

    void Recharge(){
        Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
        animatorCamera.SetTrigger("Recharge");
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
