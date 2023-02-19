using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Valuable : Collectible
{
    [SerializeField] private List<AudioClip> sounds;
    private AudioSource audioSource;
    private GameManager gameManager;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    [SerializeField] public int points;
    protected override void React()
    {
        gameManager.points += points;
        audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Count)]);
    }
}
