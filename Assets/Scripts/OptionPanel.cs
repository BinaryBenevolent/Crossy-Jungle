using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionPanel : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;

    [SerializeField] private Toggle muteToggle;
                     
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
                     
    [SerializeField] private TMP_Text muteText;

    public TMP_Text MuteText { get => muteText; set => muteText = value; }

    private void Start()
    {
        muteToggle.isOn = audioManager.IsMute;
        bgmSlider.value = audioManager.BgmVolume;
        sfxSlider.value = audioManager.SfxVolume;
        muteText.text = "Sounds Enabled";
    }
}
