using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask PlayerCollisionLayer;
    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((PlayerCollisionLayer & (1 << other.gameObject.layer)) != 0)
        {
            DialogueManager.Instance.StartDialogue(this.gameObject.name);
        }
    }
}
