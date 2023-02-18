using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Valuable : Collectible
{
    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    [SerializeField] public int points;
    protected override void React()
    {
        gameManager.points += points;
    }
}
