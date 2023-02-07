using System.Diagnostics;
using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectilMovement : MonoBehaviour
{
    public int state = 1;// 1 = waitForFire ; 2 = Fire ; 3 = inObstacle ; 4 = tutchTarget;
    GameObject foot;
    GlobalInforationscript gLS;
    public ParticleSystem particleWhenMove;
    public ParticleSystem particleWhenTouchObstacleWood;
    public ParticleSystem particleWhenTouchObstacleApple;
    public ParticleSystem particleWhenTouchTarget;
    public ParticleSystem particleWhenTouchObstacleNotStay;
    public GameObject trailRenderer;
    public Animator arrowAnimator;
    public int speed;
    public string cameraName;
    Animator cameraAnimation;
    GameObject cameraHolder;
    UnityEngine.Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        foot =  GameObject.FindWithTag("ProjectileFoot");
        gLS = GameObject.Find("GlobalInformation").GetComponent<GlobalInforationscript>();
        cameraAnimation = GameObject.Find(cameraName).GetComponent<Animator>();
        cameraHolder = GameObject.Find("CameraHolder").GetComponent<GameObject>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (state == 2){
            //quand le joueur a actionner le boutton tirer
            if (particleWhenMove != null){
                particleWhenMove.Play();
            }
            if(trailRenderer != null){
                trailRenderer.SetActive(true);
            }
            //gameObject.transform.Translate(new UnityEngine.Vector2(-speed*Time.deltaTime,0f));
            //rb.velocity = new UnityEngine.Vector2(0f, speed*Time.deltaTime);
        }else if(state == 1){
            //quand le projectile attend dans le lanceur
            gameObject.transform.rotation = foot.transform.rotation;
            gameObject.transform.position = foot.transform.position;
            rb.bodyType = RigidbodyType2D.Static;
            if(gLS.canShot == true){
                state = 2;
                
                gLS.canShot = false;
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.AddRelativeForce(new UnityEngine.Vector2(speed*Time.deltaTime, 0f));
            }
        }
    }
    
    void OnCollisionEnter2D(Collision2D  other) {
        if (state == 2){
            switch(other.gameObject.tag){
                //améliorer ce system avec de tableaux
                case "Metal":
                    OnTouchObstacleNotStay();
                    break;
                case "TargetWood":
                    OnTouchTargetNotStay(particleWhenTouchTarget);
                    break;
                case "WoodObstacle":
                    OnTouchObstacleStay(other,particleWhenTouchObstacleWood);
                    break;
                case "Apple":
                    OnTouchObstacleStay(other,particleWhenTouchObstacleApple);
                    break;
                case "arrowProjectile":
                    //other.isTrigger = true;
                    OnTouchObstacleNotStay();
                    break;

            }
        }
    }
    

    void OnTriggerEnter2D(Collider2D other){
        if (state == 2){
            switch(other.gameObject.tag){
                case "arrowProjectile":
                    OnTouchObstacleNotStay();
                    break;
            }
        }
    }

    void OnTouchObstacleNotStay(){
        rb.velocity = new UnityEngine.Vector2(-5f, 0f);
        trailRenderer.SetActive(false);
        state = 3;
        //cameraAnimation.SetTrigger("ArrowWhenTouchObstacle");
        if(particleWhenTouchObstacleNotStay != null){
            particleWhenTouchObstacleNotStay.Play();
        }
        gLS.runningNumberOfRecharge -= 1;
        if (gLS.runningNumberOfRecharge <= 0){
            gLS.loose = true;
        }
        //StartCoroutine(End());

    }
    void OnTouchObstacleStay(Collision2D  other,ParticleSystem particleWhenTouchObstacle){
        
        gameObject.GetComponent<Collider2D>().enabled = false;
        UnityEngine.Vector2 vectorNul = new UnityEngine.Vector2(0, 0);
        rb.angularVelocity = 0;
        rb.velocity = UnityEngine.Vector2.zero; 
        rb.bodyType = RigidbodyType2D.Kinematic;
        state = 3;

        gameObject.transform.parent = other.transform;
        
        //cameraAnimation.SetTrigger("ArrowWhenTouchObstacle");
        trailRenderer.SetActive(false);
        if(particleWhenTouchObstacle != null){
            particleWhenTouchObstacle.Play();
        }
        
        gLS.runningNumberOfRecharge -= 1;
        if (gLS.runningNumberOfRecharge <= 0){
            gLS.loose = true;
            
        }

        
    }

    void OnTouchTargetNotStay(ParticleSystem particleWhenTouchObstacle){
        rb.velocity = new UnityEngine.Vector2(0, 0);
        state = 4;
        
        //gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        trailRenderer.SetActive(false);
        cameraAnimation.SetTrigger("ArrowWhenTouchTarget");

        if(particleWhenTouchObstacle != null){
            particleWhenTouchTarget.Play();
        }
        gLS.runningNumberOfRecharge -= 1;
        gLS.win = true;
        //StartCoroutine(End());
    }
    
    IEnumerator End() {
        yield return new WaitForSecondsRealtime(2);
        arrowAnimator.SetTrigger("Disparition");
        yield return new WaitForSecondsRealtime(5);
        Destroy(gameObject);
    }
}
