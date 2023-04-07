using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsToReceive : MonoBehaviour
{
    public GameObject[] spawnEffects;
    public RectTransform coinSpawnerGameObject;
    public int cost;
    public int howManyByTick;
    public int howManyRound;

    void Update()
    {
        transform.eulerAngles = new Vector3(0,0,0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.tag == "arrowProjectile")
        {
            GetComponent<Collider2D>().enabled = false;

            transform.parent = null;
            transform.eulerAngles = new Vector3(0,0,0);
            
            ProjectilMovement projectileMovement = other.transform.parent.gameObject.GetComponent<ProjectilMovement>();

            GlobalFunctions.SpawnEffect(spawnEffects,projectileMovement.spawnEffect);

            StartCoroutine(GlobalFunctions.Shake(0.15f,0.1f,0.1f,BrainGame.cameraHolder,true));

            Vector3 coinSpawnerPosition = RectTransformUtility.WorldToScreenPoint(UnityEngine.Camera.main, transform.position);

            RectTransform coinSpawner = Instantiate(coinSpawnerGameObject,coinSpawnerPosition,Quaternion.Euler(0,0,0));

            CoinSpawner coinSpawnerScript = coinSpawner.GetComponent<CoinSpawner>();

            coinSpawnerScript.totalCost = cost;
            coinSpawnerScript.howManyByTick = howManyByTick;
            coinSpawnerScript.howManyRound = howManyRound;

            coinSpawner.transform.parent = BrainGame.canvas.transform;

            StartCoroutine(GlobalFunctions.DestroyDelay(gameObject ,0.15f));
        }
    }
}
