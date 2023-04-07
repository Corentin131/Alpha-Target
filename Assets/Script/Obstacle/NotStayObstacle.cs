using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class NotStayObstacle : MonoBehaviour
{
    public GameObject[] spawnEffects;

    GameObject cameraHolder = BrainGame.cameraHolder;
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

                GlobalFunctions.SpawnEffect(spawnEffects,projectileMovement.spawnEffect);
            
                StartCoroutine(GlobalFunctions.Shake(0.09f,0.3f,0.3f,cameraHolder,true));
            }
        }
    }
}
