using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AudioManager : MonoBehaviour
{
    private static AudioSource bgmInstance;
    private static AudioSource sfxInstance;

    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource sfx;

    public bool IsMute { get => bgm.mute; }

    public float BgmVolume { get => bgm.volume; }
    public float SfxVolume { get => sfx.volume; }

    private void Start()
    {
        if(bgmInstance !=null)
        {
            Destroy(bgm.gameObject);
            bgm = bgmInstance;
        }
        else
        {
            bgmInstance = bgm;
            bgm.transform.SetParent(null);
            DontDestroyOnLoad(bgm.gameObject);
        }

        if (sfxInstance != null)
        {
            Destroy(sfx.gameObject);
            sfx = sfxInstance;
        }
        else
        {
            sfxInstance = sfx;
            sfx.transform.SetParent(null);
            DontDestroyOnLoad(sfx.gameObject);
        }
    }

    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        if (bgm.isPlaying)
            bgm.Stop();

        bgm.clip = clip;
        bgm.loop = loop;
        bgm.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if(sfx.isPlaying)
            sfx.Stop();

        sfx.clip = clip;
        sfx.Play();
    }

    public void SetMute(bool value)
    {
        bgm.mute = value;
        sfx.mute = value;
    }

    public void SetBgmVolume(float value)
    {
        bgm.volume = value;
    }

    public void SetSfxVolume(float value)
    {
        sfx.volume = value;
    }

    public void SetMuteText(TMP_Text text)
    {
        if (text.text == "Sounds Enabled")
        {
            text.text = "Sounds Disabled";
        }
        else if (text.text == "Sounds Disabled")
        {
            text.text = "Sounds Enabled";
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}
