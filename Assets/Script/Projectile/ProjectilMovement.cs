using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectilMovement : MonoBehaviour
{
    public ProjectileData projectileData;
    public GameObject foot;
    public Rigidbody2D rb;
    [HideInInspector]public float speed;
    [HideInInspector] public Transform rotationBase;
    [HideInInspector]public Transform spawnEffect;
    [HideInInspector]public TrailRenderer trail;
    [HideInInspector]public  GameObject projectile;

    public int state = 1;

    void  FixedUpdate() 
    {
        if(state == 1)
        {
            rotationBase = foot.transform;
            gameObject.transform.rotation = rotationBase.rotation;
            gameObject.transform.position = rotationBase.position;
            rb.bodyType = RigidbodyType2D.Static;
        }
    }

    public void Load(ProjectileData projectileDataToLoad)
    {
        projectileData = projectileDataToLoad;
        projectile = Instantiate(projectileDataToLoad.projectile, transform.position, transform.rotation);
        projectile.transform.SetParent(transform);
        rb = projectile.GetComponent<Rigidbody2D>();
        spawnEffect = RecursiveFindChild(gameObject.transform, "SpawnEffect");
    }

    public void Shoot(float runningTime, float staticTime)
    {
        state = 2;
        gameObject.transform.SetParent(null);
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddRelativeForce(new Vector2(speed, 0f));
        trail = RecursiveFindChild(gameObject.transform, "TrailRenderer").GetComponent<TrailRenderer>();
        trail.emitting = true;
    }

   Transform RecursiveFindChild(Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
            {
                return child;
            }
            else
            {
                Transform found = RecursiveFindChild(child, childName);
                if (found != null)
                {
                    return found;
                }
            }
        }
        return null;
    }
}
