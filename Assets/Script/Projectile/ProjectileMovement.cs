using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
 
    public float speed;
    public GameObject trailRenderer;
    public ParticleSystem particleWhenMove;
    public string cameraName;
    Animator cameraAnimation;
    GameObject cameraHolder;
    public string[] nameOfTagEvent;
    public string[] whatIsTheReaction;
    public ParticleSystem[] particleWhenTouch;
    public Vector2[] magnitudeWhenTouch;
    public int state = 1;
    GameObject foot;
    GlobalInforationscript gLS;
    UnityEngine.Rigidbody2D rb;
    Transform rotationBase;

    void Start()
    {
        foot =  GameObject.FindWithTag("ProjectileFoot");
        gLS = GameObject.Find("GlobalInformation").GetComponent<GlobalInforationscript>();
        cameraAnimation = GameObject.Find(cameraName).GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        cameraHolder = GameObject.Find("CameraHolder");
        Debug.Log(cameraHolder);
    }
    
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
            gameObject.transform.rotation = rotationBase.rotation;
        }else if(state == 1){
            //quand le projectile attend dans le lanceur
            rotationBase =  foot.transform;
            gameObject.transform.rotation = rotationBase.rotation;
            gameObject.transform.position = rotationBase.position;
            rb.bodyType = RigidbodyType2D.Static;

            if(gLS.canShot == true){
                state = 2;
                gLS.canShot = false;
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.AddRelativeForce(new Vector2(speed*Time.deltaTime, 0f));
            }
        }
    }
    void OnCollisionEnter2D(Collision2D  other) {
        foreach (int index in Enumerable.Range(0, nameOfTagEvent.Length)){
            if(nameOfTagEvent[index] == other.gameObject.tag && state == 2){
                switch(whatIsTheReaction[index]){
                    case "NotStay":
                        OnTouchObstacleNotStay(particleWhenTouch[index],false,magnitudeWhenTouch[index]);
                        break;
                    case "NotStayWin":
                        OnTouchObstacleNotStay(particleWhenTouch[index],true,magnitudeWhenTouch[index]);
                        break;
                    case "Stay":
                        OnTouchObstacleStay(particleWhenTouch[index],other,false,magnitudeWhenTouch[index]);
                        break;
                    case "StayWin":
                        OnTouchObstacleStay(particleWhenTouch[index],other,true,magnitudeWhenTouch[index]);
                        break;

                }
            }
        }
    }

    void OnTouchObstacleNotStay(ParticleSystem particle,bool ifWin,Vector2 magnitude){

        rb.velocity = new Vector2(-5f, 0f);
        //rb.angularVelocity = 1000;
        trailRenderer.SetActive(false);
        StartCoroutine(gLS.Shake(0.09f,magnitude.x,magnitude.y,cameraHolder,true));
        cameraAnimation.SetTrigger("Shoot");
        gLS.runningNumberOfRecharge -= 1;
        if(particle != null){
            particle.Play();
        }
        if (gLS.runningNumberOfRecharge <= 0){
            gLS.loose = true;
        }
        if(ifWin == true){
            gLS.win = true;
            StartCoroutine(gLS.Shake(0.1f,magnitude.x,magnitude.y,cameraHolder,true));
        }else{
            state = 3;
        }
    }

    void OnTouchObstacleStay(ParticleSystem particle,Collision2D  other,bool ifWin,Vector2 magnitude){
        gameObject.transform.rotation = rotationBase.rotation;
        state = 3;
        gameObject.GetComponent<Collider2D>().enabled = false;
        Vector2 vectorNul = new Vector2(0, 0);
        rb.angularVelocity = 0;
        rb.velocity = Vector2.zero; 
        rb.bodyType = RigidbodyType2D.Kinematic;
        trailRenderer.SetActive(false);
        gameObject.transform.parent = other.transform;
        StartCoroutine(gLS.Shake(0.09f,magnitude.x,magnitude.y,cameraHolder,true));
        cameraAnimation.SetTrigger("Shoot");
        gLS.runningNumberOfRecharge -= 1;
        if(particle != null){
            particle.Play();
        }
        if (gLS.runningNumberOfRecharge <= 0){
            gLS.loose = true;
        }
        if(ifWin == true){
            StartCoroutine(gLS.Shake(0.1f,magnitude.x,magnitude.y,cameraHolder,true));
            gLS.win = true;
        }
    }
}