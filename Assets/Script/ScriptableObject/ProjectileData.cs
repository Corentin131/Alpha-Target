using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ProjectileData", menuName = "Game/ProjectileData")]
public class ProjectileData : ScriptableObject
{
    public new string name;
    public string description;
    public GameObject projectile;
    public float rechargeTime;
}
