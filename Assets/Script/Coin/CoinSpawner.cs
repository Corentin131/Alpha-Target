using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    GameObject canvas = BrainGame.canvas;
    public GameObject coinToSpawn;
    public int totalCost;
    public int  howManyByTick;
    public int howManyRound;
    public int distance;

    GameObject coinUI = BrainGame.coinUI;
    Transform CoinReceptionist;
    Animator coinAnimator;
    void Start()
    {   
        coinUI = GlobalFunctions.RecursiveFindChild(canvas.transform , "Coin").gameObject;
        CoinReceptionist = GlobalFunctions.RecursiveFindChild(coinUI.transform, "CoinReceptionist");
        coinAnimator = coinUI.GetComponent<Animator>();

        StartCoroutine(Spawn());
    }
    public IEnumerator Spawn()
    {
        float coinSpwned = 0;
    
        while(coinSpwned < howManyRound)
        {
            foreach (int value in Enumerable.Range(1, howManyByTick))
            {
                float x  = Random.Range(-distance,distance);
                float y = Random.Range(-distance,distance);

                GameObject coin = Instantiate(coinToSpawn , transform.position , transform.rotation);

                coin.transform.parent = transform;

                Coin coinScript =  coin.GetComponent<Coin>();

                coinScript.CoinReceptionist = CoinReceptionist;
                coinScript.cost =  (float)totalCost/(howManyByTick*howManyRound);
                coinScript.coinAnimator = coinAnimator;
                coin.transform.localPosition = new Vector2(x,y);
            }

            coinSpwned += 1;
            
            yield return null;
        }
    }

    
}
