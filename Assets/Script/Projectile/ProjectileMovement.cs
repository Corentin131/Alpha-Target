using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileMovement : MonoBehaviour
{
    public Transform WorldObject; 
    public RectTransform m_image;
    void Update()
    {
        Vector3 screenPosition = RectTransformUtility.WorldToScreenPoint(UnityEngine.Camera.main, WorldObject.position);
        m_image.transform.position = screenPosition;
    }
}
