using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : Interactable
{
    [SerializeField] private Vector3 hidePosition;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            React();
            transform.position = hidePosition;
        }
    }
}



