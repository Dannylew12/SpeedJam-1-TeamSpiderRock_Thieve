using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Door : MonoBehaviour
{

    private bool haveKey = false;
    public void PickupKey()
    {
        haveKey = true;
    }

    private const float openAngle = 150f;
    private bool opened = false;
    private void Open()
    {
        if (!opened)
        {
            opened = true;
            /// 0th child is guaranteed to be the rotation point
            transform.RotateAround(transform.GetChild(0).position, transform.up, openAngle);
        }
    }

    [SerializeField] TMP_Text lockedText;
    [SerializeField] TMP_Text openText;
    /// display if door can be opened/not
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerNear = true;
            if (haveKey)
            {
                openText.gameObject.SetActive(true);
            }
            else
            {
                lockedText.gameObject.SetActive(true);
            }
        }
    }
    /// take the text away
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerNear = false;
            if (haveKey)
            {
                openText.gameObject.SetActive(false);
            }
            else
            {
                lockedText.gameObject.SetActive(false);
            }
        }
    }

    private bool playerNear = false;
    private void Update()
    {
        if (haveKey && playerNear && Input.GetKeyDown(KeyCode.E))
        {
            Open();
        }
    }

}
