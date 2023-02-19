using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{

    /// WW - wood walk
    /// WR - wood run
    /// SW - stone walk
    /// SR - stone run
    public enum SFXTYPE { WW, WR, SW, SR, DEFAULT }
    private SFXTYPE type = SFXTYPE.DEFAULT;
    private AudioSource sfxSource;
    private void Start()
    {
        sfxSource = GetComponent<AudioSource>();
    }

    [SerializeField] private List<AudioClip> woodWalk;
    [SerializeField] private List<AudioClip> woodRun;
    [SerializeField] private List<AudioClip> stoneWalk;
    [SerializeField] private List<AudioClip> stoneRun;
    public void SetAudio(SFXTYPE nType)
    {
        type = nType;
    }

    public void PlayAudio()
    {
        /// do not override current audio
        if (sfxSource.isPlaying) return;


        if (type == SFXTYPE.WW)
        {
            sfxSource.clip = woodWalk[Random.Range(0, woodWalk.Count)];
        }
        if (type == SFXTYPE.WR)
        {
            sfxSource.clip = woodRun[Random.Range(0, woodRun.Count)];
        }
        if (type == SFXTYPE.SW)
        {
            sfxSource.clip = stoneWalk[Random.Range(0, stoneWalk.Count)];
        }
        if (type == SFXTYPE.SR)
        {
            sfxSource.clip = stoneRun[Random.Range(0, stoneRun.Count)];
        }
        sfxSource.PlayOneShot(sfxSource.clip);
    }

    public void StopAudio()
    {
        sfxSource.Stop();
    }

}
