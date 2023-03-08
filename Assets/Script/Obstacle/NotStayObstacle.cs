using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class NotStayObstacle : MonoBehaviour
{
    public GlobalInforationscript gLS;
    public GameObject cameraHolder;
    public GameObject[] spawnEffect;
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            Rigidbody2D rb = other.collider.transform.gameObject.GetComponent<Rigidbody2D>();
            ProjectilMovement projectileMovement =  other.collider.transform.parent.gameObject.GetComponent<ProjectilMovement>();
            if(projectileMovement.state == 2)
            {
                Debug.Log(rb.angularVelocity);
                if(rb.angularVelocity != 0)
                {
                    rb.angularVelocity = rb.angularVelocity/Math.Abs(rb.angularVelocity)*1000;
                }
                rb.velocity = Vector2.zero; 

                projectileMovement.trail.enabled = false;
                projectileMovement.state = 3;

                foreach(GameObject objectToSpawn in spawnEffect)
                {
                    Transform rotation = projectileMovement.spawnEffect;
                    if(objectToSpawn.tag == "Text")
                    {
                        rotation.eulerAngles = new Vector3(0,0,0);
                    }
                    Instantiate(objectToSpawn, rotation.position,rotation.rotation);
                }
            
                StartCoroutine(gLS.Shake(0.09f,0.3f,0.3f,cameraHolder,true));
            }
        }
    }
}
