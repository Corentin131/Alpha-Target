using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Transform CoinReceptionist;
    public float speed;
    public float cost;
    public Animator coinAnimator;
    float decaySpeed;
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position , CoinReceptionist.position,speed);
        
        if(Vector3.Distance(transform.position, CoinReceptionist.position) < 0.1f)
        {
            Bank.coins += cost;
            coinAnimator.SetTrigger("Earning");
            Vibrator.Vibrate(20);
            Destroy(gameObject);
        }
    }
}
