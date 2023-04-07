using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BrainGame
{
    public static bool canShot = false;
    public static bool canRecharge = false;
    public static bool win = false;
    public static bool loose = false;
    public static int numberOfRecharge;
    public static float timerRecharge;
    public static GameObject coinUI;
    public static GameObject canvas;
    public static GameObject cameraHolder;

    public static void Initialize(GameObject coinUIGameObject, GameObject canvasGameObject, GameObject cameraHolderGameObject)
    {
        coinUI = coinUIGameObject;
        canvas = canvasGameObject;
        cameraHolder = cameraHolderGameObject;
    }
}
