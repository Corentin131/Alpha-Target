using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GlobalInforationscript gLS;
    public PlayerData playerData;
    public GameObject cameraObject;
    public GameObject cameraHolder;
    public GameObject defaultProjectile;
    public GameObject playerCenter;

    public int state = 1;
    GameObject player;
    Animator playerAnimator;
    Animator cameraAnimator;
    ProjectilMovement projectileScript;

    void Start()
    {
        player = LoadPlayer();
        gLS.timerRecharge = playerData.rechargeDelay;
        gLS.runningNumberOfRecharge = playerData.numberOfRecharge;
        gLS.staticNumberOfRecharge = playerData.numberOfRecharge;
        cameraAnimator = cameraObject.GetComponent<Animator>();
        cameraAnimator.SetTrigger("Recharge");
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Movement(touch);

            if (state == 1)
            {
                Recharge();
            }

            if (touch.phase == TouchPhase.Ended && state == 2)
            {
                Shoot();
            }
        }
        else if (Input.touchCount <= 0 && state == 2)
        {
            Shoot();
        }
    }

    GameObject LoadPlayer()
    {
        GameObject player = Instantiate(playerData.player, transform.position, transform.rotation);
        player.transform.SetParent(transform);
        playerAnimator = player.GetComponent<Animator>();
        return player;
    }
    
    void LoadProjectile()
    {
        GameObject projectile =  Instantiate(defaultProjectile, transform.position, transform.rotation);
        projectileScript = projectile.GetComponent<ProjectilMovement>();
        projectileScript.Load(playerData.projectileData);
        projectileScript.foot = GameObject.Find("ProjectileFoot").gameObject;
        projectileScript.speed = playerData.powerOfShoot;
        projectileScript.gLS = gLS;
        Debug.Log(projectileScript.speed);
    }

    void Recharge()
    {
        state = 0;
        LoadProjectile();
        StartCoroutine(PlayAnimatorDelay(playerAnimator,"Recharge",playerData.projectileData.rechargeTime));
        StartCoroutine(TimerBeforeShoot(playerData.rechargeDelay));
    }

    void Shoot() 
    {
        state = 1;
        playerAnimator.SetTrigger("Shoot");
        projectileScript.Shoot();
        gLS.timerRecharge = playerData.rechargeDelay;
        gLS.runningNumberOfRecharge -= 1;
        StartCoroutine(gLS.Shake(0.09f,0.1f,0.1f,cameraHolder,true));
    }

    void Movement(Touch touch)
    {
        
       
        Debug.Log(Screen.width);
        float angle = ((touch.position.x / Screen.width) * 105)-50;
        float angleResult = angle * Time.deltaTime;
        playerCenter.transform.localEulerAngles = new Vector3(0f, 0f, angle);
    }

    IEnumerator TimerBeforeShoot(float maxTime)
    {
        float runningTime = maxTime;

        while (runningTime > 0)
        {
            runningTime -= Time.unscaledDeltaTime;
            gLS.timerRecharge = runningTime;
            yield return null;
        }
        state = 2;
    }
    IEnumerator PlayAnimatorDelay(Animator animator, string name, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        animator.SetTrigger(name);
    }
}