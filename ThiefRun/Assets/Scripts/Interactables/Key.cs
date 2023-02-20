using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Key : Collectible
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private List <Door> doors;
    protected override void React()
    {
        if (text != null)
            text.gameObject.SetActive(true);
		foreach (Door door in doors)
		door.PickupKey();
    }
}
