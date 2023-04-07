using System.Net.Mime;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetSlow : MonoBehaviour
{
    public GameObject[] objectsToActivate;
    public GameObject[] objectsToDisable;
    public GameObject[] Effects;
    public Transform spawnEffect;
    List<GameObject> myArrowGameObject = new List<GameObject>();
    public Camera principalCamera;
    public Camera secondCamera;

    GameObject principalCameraHolder;
    GameObject secondCameraHolder;

    void Start()
    {
        principalCameraHolder = principalCamera.transform.parent.gameObject;
        secondCameraHolder = secondCamera.transform.parent.gameObject;
    }
    void OnTriggerEnter2D(Collider2D  other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            ProjectilMovement projectileMovement = other.transform.parent.gameObject.GetComponent<ProjectilMovement>();

            if(projectileMovement.state == 2)
            {
                projectileMovement.state = 3;

                myArrowGameObject.Add(other.gameObject);

                gameObject.transform.rotation = other.transform.rotation;
                
                other.gameObject.GetComponent<Collider2D>().enabled = false;

                BrainGame.win = true;

                StartCoroutine(GlobalFunctions.SlowDawn(0.005f,0.8f));
                
                foreach(GameObject objectToSpawn in Effects)
                {
                    if(objectToSpawn.tag == "Text")
                    {
                        spawnEffect.eulerAngles = new Vector3(0,0,0);
                    }
                    Instantiate(objectToSpawn, spawnEffect.position,spawnEffect.rotation);
                }

                foreach (GameObject objectToActivate in objectsToActivate)
                {
                    objectToActivate.SetActive(true);
                    Rigidbody2D rb = objectToActivate.GetComponent<Rigidbody2D>();
                    rb.bodyType = RigidbodyType2D.Dynamic;
                    rb.AddRelativeForce(new Vector2(400000f, 0f));
                }

                foreach (GameObject objectToDisable in objectsToDisable)
                {
                    objectToDisable.GetComponent<SpriteRenderer>().enabled = false;
                }
                
                StartCoroutine(GlobalFunctions.Shake(0.1f , 0.08f ,0.08f,principalCameraHolder,true));

                Vibrator.Vibrate(50);
            } 
        }   
    }


}

