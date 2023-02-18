using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorEnd : Door
{

    [SerializeField] private TMP_Text winText;
    [SerializeField] private TMP_Text clearText;
    [SerializeField] private TMP_Text pointsDisplay;
    [SerializeField] private TMP_Text timeDisplay;
    protected override void Open()
    {
        //base.Open();
        GameObject.FindObjectOfType<GameManager>().winState = true;

        winText.gameObject.SetActive(true);
        clearText.gameObject.SetActive(false);

        pointsDisplay.text = "YOUR SCORE:\n" + pointsDisplay.text;
        pointsDisplay.fontSize = 40f;
        pointsDisplay.alignment = TextAlignmentOptions.Center;
        pointsDisplay.transform.localPosition = new Vector3(150, 96, 0);

        timeDisplay.text = "YOUR TIME:\n" + timeDisplay.text;
        timeDisplay.fontSize = 40f;
        timeDisplay.alignment = TextAlignmentOptions.Center;
        timeDisplay.transform.localPosition = new Vector3(-103, -30, 0);
    }

}
