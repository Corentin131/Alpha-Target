using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayObstacle : MonoBehaviour
{   
    public GlobalInforationscript gLS;
    public GameObject cameraHolder;
    public GameObject[] spawnEffect;
    public Animator cameraAnimator;
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            Rigidbody2D rb = other.transform.gameObject.GetComponent<Rigidbody2D>();
            ProjectilMovement projectileMovement = other.transform.parent.gameObject.GetComponent<ProjectilMovement>();
            if (projectileMovement.state == 2)
            {
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.angularVelocity = 0;
                rb.velocity = Vector2.zero; 

                other.transform.parent.transform.parent = gameObject.transform;

                other.GetComponent<Collider2D>().enabled = false;
                projectileMovement.trail.emitting = false;

                //cameraAnimator.SetTrigger("Shoot");
                
                foreach(GameObject objectToSpawn in spawnEffect)
                {
                    Transform positionEffect = projectileMovement.spawnEffect;
                    /*
                    if(objectToSpawn.tag == "Text")
                    {
                        positionEffect.eulerAngles = new Vector3(0,0,0);
                        Instantiate(objectToSpawn, positionEffect.position,positionEffect.localRotation);
                    }
                    */
                    Instantiate(objectToSpawn, positionEffect.position,positionEffect.localRotation);
                }
                
                StartCoroutine(gLS.Shake(0.09f,0.2f,0.2f,cameraHolder,true));
            }
        }
    }
}
