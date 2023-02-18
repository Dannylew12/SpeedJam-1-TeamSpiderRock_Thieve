using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Key : Collectible
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Door door;
    protected override void React()
    {
        if (text != null)
            text.gameObject.SetActive(true);
        door.PickupKey();
    }
}
