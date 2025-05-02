using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statHandler : MonoBehaviour
{
    [Range(1f,20f)] [SerializeField] private float movementSpeed = 3f;
    [Range(1,100)] [SerializeField] private int health = 10;

    public float movement
    {
     get => movementSpeed;
     set => movementSpeed = Mathf.Clamp(value, 0f, 20f);
    }

    public int Health
    {
        get => health;
        set => health = Mathf.Clamp(value, 0, 1000);
    }
}
