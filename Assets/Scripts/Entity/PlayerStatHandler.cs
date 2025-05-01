using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatHandler : MonoBehaviour
{
    [Range(1f,20f)] [SerializeField] private float movementSpeed = 3f;

    public float movement
    {
     get => movementSpeed;
     set => movementSpeed = Mathf.Clamp(value, 0f, 20f);
    }
}
