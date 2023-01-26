using System.Diagnostics;
using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectilMovement : MonoBehaviour
{
    public bool fire = false;
    public bool inObstacle = false;
    public bool tutchTarget = false;
    public bool waitForFire = true;
    GameObject foot;
    GlobalInforationscript gLS;
    public ParticleSystem particleWhenMove;
    public ParticleSystem particleWhenTouchObstacle;
    public ParticleSystem particleWhenTouchTarget;
    public ParticleSystem particleWhenTouchObstacleNotStay;
    public GameObject trailRenderer;
    public int speed;
    public string cameraName;
    Animator cameraAnimation;
    UnityEngine.Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        foot =  GameObject.FindWithTag("ProjectileFoot");
        gLS = GameObject.Find("GlobalInformation").GetComponent<GlobalInforationscript>();
        cameraAnimation = GameObject.Find(cameraName).GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //m_Rigidbody.AddForce(transform.up * m_Thrust);
        if (fire == true){
            //quand le joueur a actionner le boutton tirer
            if (particleWhenMove != null){
                particleWhenMove.Play();
            }
            if(trailRenderer != null){
                trailRenderer.SetActive(true);
            }
            gameObject.transform.Translate(new UnityEngine.Vector2(-speed*Time.deltaTime,0f));
        }else if(waitForFire == true){
            //quand le projectile attend dans le lanceur
            gameObject.transform.rotation = foot.transform.rotation;
            gameObject.transform.position = foot.transform.position;
            if(gLS.canShot == true){
                fire = true;
                waitForFire = false;
                tutchTarget = false;
                inObstacle = false;
                gLS.canShot = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        switch(other.gameObject.tag){
            case "WoodObstacle":
                OnTouchObstacleStay(other);
                break;
            case "Scie":
                OnTouchObstacleNotStay();
                break;
            case "TargetWood":
                OnTouchTargetNotStay();
                break;
        }
    }
    void OnTouchObstacleNotStay(){
        rb.gravityScale = 1;
        rb.AddRelativeForce(new UnityEngine.Vector2(0f, -500f));
        rb.AddTorque(50f);
        trailRenderer.SetActive(false);
        fire = false;
        tutchTarget = true;
        cameraAnimation.SetTrigger("ArrowWhenTouchObstacle");
        if(particleWhenTouchObstacleNotStay != null){
            particleWhenTouchObstacleNotStay.Play();
        }
        gLS.runningNumberOfRecharge -= 1;
        if (gLS.runningNumberOfRecharge <= 0){
            gLS.loose = true;
        }
        StartCoroutine(End());
    }
    void OnTouchObstacleStay(Collider2D other){
        rb.velocity = new UnityEngine.Vector2(0, 0);
        if (tutchTarget != true){
            inObstacle = true;
            fire = false;
            waitForFire = false;
            gameObject.transform.parent = other.transform;
            cameraAnimation.SetTrigger("ArrowWhenTouchObstacle");
            trailRenderer.SetActive(false);
            if(particleWhenTouchObstacle != null){
                particleWhenTouchObstacle.Play();
            }
            
            gLS.runningNumberOfRecharge -= 1;
            if (gLS.runningNumberOfRecharge <= 0){
                gLS.loose = true;
            }

        }
    }

    void OnTouchTargetNotStay(){
        tutchTarget = true;
        inObstacle = false;
        fire = false;
        waitForFire = false;

        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        trailRenderer.SetActive(false);
        cameraAnimation.SetTrigger("ArrowWhenTouchTarget");

        if(particleWhenTouchObstacle != null){
            particleWhenTouchTarget.Play();
        }
        gLS.runningNumberOfRecharge -= 1;
        gLS.win = true;
        StartCoroutine(End());
    }
    IEnumerator End() {
        yield return new WaitForSecondsRealtime(3);
        Destroy(gameObject);
    }
}
