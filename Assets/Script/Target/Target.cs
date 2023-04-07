using System;
using UnityEngine;


public class Target : MonoBehaviour
{
    public GameObject[] objectsToActivate;
    public GameObject[] objectsToDisable;
    public GameObject[] spawnEffects;
    public Transform spawnEffect;
    public GameObject cameraHolder;

    public void Slice(GameObject projectile)
    {
        ProjectilMovement projectileMovement = projectile.transform.parent.gameObject.GetComponent<ProjectilMovement>();

        if(projectileMovement.state == 2)
        {
            projectileMovement.state = 3;
            
            projectile.gameObject.GetComponent<Collider2D>().enabled = false;

            BrainGame.win = true;

            GlobalFunctions.SpawnEffect(spawnEffects,spawnEffect);

            foreach (GameObject objectToActivate in objectsToActivate)
            {
                Rigidbody2D rb = objectToActivate.GetComponent<Rigidbody2D>();
                Vector2 force = objectToActivate.GetComponent<StayObstacle>().force;

                objectToActivate.GetComponent<SpriteRenderer>().enabled = true;
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.AddRelativeForce(force);
                rb.angularVelocity = -(Mathf.Sign(force.x))*50;
            }

            foreach (GameObject objectToDisable in objectsToDisable)
            {
                objectToDisable.GetComponent<SpriteRenderer>().enabled = false;
            }
            
            StartCoroutine(GlobalFunctions.Shake(0.1f , 0.4f ,0.4f,cameraHolder,true));

            Vibrator.Vibrate(50);
            } 
    }   
}

