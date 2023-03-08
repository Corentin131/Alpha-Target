using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="ObstacleData",menuName="Game/ObstacleData")]
public class ObstacleData : ScriptableObject
{
    public new string name;
    public string type;
    public GameObject obstacle;
}
