using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StayObstacle : MonoBehaviour
{   
    public GameObject[] spawnEffects;
    public Target target;
    public Vector2 force;
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

                GlobalFunctions.SpawnEffect(spawnEffects,projectileMovement.spawnEffect);
                
                StartCoroutine(GlobalFunctions.Shake(0.09f,0.3f,0.3f,BrainGame.cameraHolder,false));
            }

            if(target != null)
            {
                target.Slice(other.gameObject);
                other.transform.parent.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            }
        }
    }
}
