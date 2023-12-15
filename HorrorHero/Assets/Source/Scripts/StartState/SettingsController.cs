using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Kuhpik;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsController : GameSystem, IIniting
{
    [SerializeField] private RectTransform SoundHub;
    [SerializeField] private RectTransform VibrationHub;

    [SerializeField] private GameObject soundOff;
    [SerializeField] private GameObject soundOn;
    [SerializeField] private GameObject vibrationOn;
    [SerializeField] private GameObject vibrationOff;

    [SerializeField] private float distance;

    [SerializeField] private AudioMixer mix;

    private float soundHubAnchorPositionY;
    private float vibrationHubAnchorPositionY;

    private bool click;

    void IIniting.OnInit()
    {
        soundHubAnchorPositionY = SoundHub.anchoredPosition.y;
        vibrationHubAnchorPositionY = VibrationHub.anchoredPosition.y;
        click = false;

        if (player.level == 0)
        {
            player.sound = true;
            player.vibration = true;
        }
        
        soundOn.SetActive(player.sound);
        soundOff.SetActive(!player.sound);
        vibrationOn.SetActive(player.vibration);
        vibrationOff.SetActive(!player.vibration);
    }
    
    public void SoundOn()
    {
        soundOff.SetActive(true);
        soundOn.SetActive(false);
        player.sound = false;
        mix.SetFloat("Volume", -80);
    }

    public void SoundOf()
    {
        soundOff.SetActive(false);
        soundOn.SetActive(true);
        player.sound = true;
        mix.SetFloat("Volume", 0);
    }

    public void VibrationOn()
    {
        vibrationOff.SetActive(true);
        vibrationOn.SetActive(false);
        player.vibration = false;
    }

    public void VibrationOf()
    {
        vibrationOff.SetActive(false);
        vibrationOn.SetActive(true);
        player.vibration = true;
    }

    public void SettingClick()
    {
        click = !click;

        if (click)
        {
            VibrationHub.DOAnchorPosY(-VibrationHub.sizeDelta.y - distance, 0.5f);
            SoundHub.DOAnchorPosY(-SoundHub.sizeDelta.y*2 - distance*2, 0.5f);
        }
        else
        {
            VibrationHub.DOAnchorPosY(vibrationHubAnchorPositionY, 0.5f);
            SoundHub.DOAnchorPosY(soundHubAnchorPositionY, 0.5f);
        }
    }
}
