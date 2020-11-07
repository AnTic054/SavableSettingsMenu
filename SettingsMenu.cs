using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [Header("Refrences")]
    public AudioMixer audioMixer;
    public TMP_Dropdown qualityDropDown;
    public TMP_Dropdown resDropdown;
    public Toggle fxToggle;
    public Toggle musicToggle;
    public Toggle fullscreenToggle;
    public Slider masterVolumeSlider;


    Resolution[] resolutions;


    [Header("Key Values")]
    public bool isFullScreen;

    public int qualityIndex;
    public int resIndex;

    public float masterVolume;


    private void Start()
    {
        CreateResolutions();
        LoadSettings();
    }


    public void SetMasterVolume()
    {
        masterVolume = masterVolumeSlider.value;
        audioMixer.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
    }

    public void ToggleMusic()
    {
        if (musicToggle.isOn)
        {
            audioMixer.SetFloat("MusicVolume", 0f);
        }
        else if (!musicToggle.isOn)
        {
            audioMixer.SetFloat("MusicVolume", -80f);
        }

        PlayerPrefs.SetInt("MusicVolume", musicToggle.isOn ? 1 : 0);
    }
    public void ToggleFX()
    {
        if (fxToggle.isOn)
        {
            audioMixer.SetFloat("FXvolume", 0f);
        }
        else if (!fxToggle.isOn)
        {
            audioMixer.SetFloat("FXvolume", -80f);
        }
        PlayerPrefs.SetInt("FXVolume", fxToggle.isOn ? 1 : 0);
    }

    public void SetGraphics()
    {
        qualityIndex = qualityDropDown.value;
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Graphics", qualityIndex);
    }

    public void SetFullScreen()
    {
        isFullScreen = fullscreenToggle.isOn;

        Screen.fullScreen = fullscreenToggle.isOn;

        PlayerPrefs.SetInt("SetFullsccreen", fullscreenToggle.isOn ? 1 : 0);
    }
    
    public void SetResolution()
    {
        resIndex = resDropdown.value;
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height,isFullScreen);
    }

    public void CreateResolutions()
    {
        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();

        List<string> resOptions = new List<string>();
        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resOptions.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }
        resDropdown.AddOptions(resOptions);
        resDropdown.value = currentResIndex;
        resDropdown.RefreshShownValue();

    }



    public void SaveSettings()
    {
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");

        musicToggle.isOn = PlayerPrefs.GetInt("MusicVolume") == 1;

        fxToggle.isOn = PlayerPrefs.GetInt("FXVolume") == 1;

        fullscreenToggle.isOn = PlayerPrefs.GetInt("SetFullsccreen") == 1;

        qualityDropDown.value = PlayerPrefs.GetInt("Graphics");
    }
}
