using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    private Vector3 offset = new Vector3(0f, 0f, -10f);
        
    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
            
    }
}