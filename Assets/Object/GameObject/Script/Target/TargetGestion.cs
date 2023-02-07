using System.Net.Mime;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetGestion : MonoBehaviour
{
    public GlobalInforationscript gLS;
    public GameObject[] objectsToActivate;
    public GameObject[] objectsToDisable;
    public List<GameObject> myArrowGameObject = new List<GameObject>();
    public bool life = false;
    public int lifes = 2;
    void OnTriggerEnter2D(Collider2D  other) {
        ProjectileMovement projectileMovement = other.gameObject.GetComponent<ProjectileMovement>();
        if(projectileMovement.state == 2){
            myArrowGameObject.Add(other.gameObject);
            if(life == true){
                lifes -= 1;
                if (lifes <= 0){
                    DestroyTarget(objectsToActivate,objectsToDisable,myArrowGameObject.ToArray());
                    gLS.win = true;
                }
            }
        }
    }
    void OnCollisionEnter2D(Collision2D  other) {
        ProjectileMovement projectileMovement = other.gameObject.GetComponent<ProjectileMovement>();
        UnityEngine.Debug.Log(projectileMovement.state);
        if(projectileMovement.state == 2){
            if (life == false){
                DestroyTarget(objectsToActivate,objectsToDisable,myArrowGameObject.ToArray());
                projectileMovement.state = 3;
                gLS.win = true;
        }   
    }
    }
    IEnumerator End() {
        yield return new WaitForSecondsRealtime(1);
        Destroy(gameObject);
    }

    IEnumerator TakeScreenShot()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        yield return new WaitForEndOfFrame();
        //WinMenuSpriteScreenShot.texture = ScreenCapture.CaptureScreenshotAsTexture();
        //mySprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        // do something with texture

        // cleanup
        //Object.Destroy(texture);
    }

    void DestroyTarget(GameObject[] objectsToActivate,GameObject[] objectsToDisable,GameObject[] myArrowGameObjects){
        int num = 0;
        foreach (GameObject myArrowGameObject in myArrowGameObjects){
            num ++;
            //if (num % 2 == 1){
                UnityEngine.Debug.Log(num % 2);
                Rigidbody2D rb = myArrowGameObject.GetComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.angularVelocity = 30;
            //}
        }
        foreach (GameObject objectToActivate in objectsToActivate)
        {
            objectToActivate.SetActive(true);
            Rigidbody2D rb = objectToActivate.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.AddRelativeForce(new Vector2(400f, 0f));
        }
        foreach (GameObject objectToDisable in objectsToDisable)
        {
            objectToDisable.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

}
