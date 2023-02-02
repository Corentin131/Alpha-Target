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

    void OnCollisionEnter2D(Collision2D  other) {
        //SetActiveGameObject(objectsToActivate,objectsToDisable);
        ProjectilMovement projectileMovement = other.gameObject.GetComponent<ProjectilMovement>();
        if(projectileMovement.state == 2){
            SetActiveGameObject(objectsToActivate,objectsToDisable);
            projectileMovement.state = 4;
            StartCoroutine(End());
        //gLS.win = true;
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

    void SetActiveGameObject(GameObject[] objectsToActivate,GameObject[] objectsToDisable){
        foreach (GameObject objectToActivate in objectsToActivate)
        {
            objectToActivate.SetActive(true);
            objectToActivate.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(500f, 0f));
        }
        foreach (GameObject objectToDisable in objectsToDisable)
        {
            objectToDisable.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

}
