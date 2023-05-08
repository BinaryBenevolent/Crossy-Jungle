using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource sfx;

    public void playBGM(AudioClip clip, bool loop = true)
    {
        if (bgm.isPlaying)
            bgm.Stop();

        bgm.clip = clip;
        bgm.loop = loop;
        bgm.Play();
    }

    public void playSFX(AudioClip clip)
    {
        if(sfx.isPlaying)
            sfx.Stop();

        sfx.clip = clip;
        sfx.Play();
    }
}
