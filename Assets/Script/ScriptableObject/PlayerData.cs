using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="PlayerData",menuName="Game/PlayerData")]
public class PlayerData : ScriptableObject
{
    public new string name;
    public string description;
    public GameObject player;
    public float powerOfShoot;
    public int numberOfRecharge;
    public ProjectileData projectileData;
    public float rechargeDelay;
}
