using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Collectible
{
    [SerializeField] private Door door;
    protected override void React()
    {
        door.PickupKey();
    }
}
