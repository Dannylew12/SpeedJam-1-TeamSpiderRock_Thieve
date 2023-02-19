using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarmed : MonoBehaviour
{

    private bool triggered = false;
    [SerializeField] private AudioClip alarmedTheme;
    public void PlayAlarmed()
    {
        if (triggered) return;

        AudioSource mainTheme = GetComponent<AudioSource>();
        mainTheme.clip = alarmedTheme;
        mainTheme.Play();
    }

}
