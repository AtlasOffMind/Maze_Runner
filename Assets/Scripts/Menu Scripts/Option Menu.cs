using System.Collections;
using System.Collections.Generic;
using MazeRunner;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    public Slider sliderVolume;
    public float sliderValue;
    public Image imagenMute;

    public Toggle toggle;

    public Slider brightnessSlider;
    public float brightnessSliderValue;
    public Image panelBrightness;

    public TMP_Dropdown dropdownQuality;
    public int quality;

    public TMP_Dropdown dropdownResolution;
    Resolution[] resolutions;




    void Start()
    {
        #region Volume
        sliderVolume.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume = sliderVolume.value;
        IsMute();
        #endregion

        #region Screen
        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else toggle.isOn = false;
        #endregion

        #region Brightness
        brightnessSlider.value = PlayerPrefs.GetFloat("brillo", 0.5f);
        panelBrightness.color = new Color(panelBrightness.color.r, panelBrightness.color.g, panelBrightness.color.b, brightnessSlider.value);
        #endregion

        #region Brightness
        quality = PlayerPrefs.GetInt("numeroDeCalidad", 30);
        dropdownQuality.value = quality;
        SetingQuality();
        #endregion

        #region Resolutions
        ReviewResolutions();
        #endregion
    }

    public void ChangeSliderVolume()
    {

        PlayerPrefs.SetFloat("volumenAudio", sliderValue);
        AudioListener.volume = sliderVolume.value;
        IsMute();

    }
    public void ChangeSliderBrightness()
    {
        PlayerPrefs.SetFloat("brillo", brightnessSliderValue);
        panelBrightness.color = new Color(panelBrightness.color.r, panelBrightness.color.g, panelBrightness.color.b, brightnessSlider.value);
    }
    public void IsMute()
    {
        if (sliderVolume.value == 0)
        {
            imagenMute.enabled = true;
        }
        else imagenMute.enabled = false;
    }

    public void ActivateFullScreen()
    {
        Screen.fullScreen = true;
    }

    public void SetingQuality()
    {
        QualitySettings.SetQualityLevel(dropdownQuality.value);
        PlayerPrefs.SetInt("numeroDeCalidad", quality);
        quality = dropdownQuality.value;
    }

    public void ReviewResolutions()
    {
        resolutions = Screen.resolutions;
        dropdownResolution.ClearOptions();
        List<string> options = new List<string>();
        int currentResolution = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (Screen.fullScreen && resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolution = i;
            }
        }
        dropdownResolution.AddOptions(options);
        dropdownResolution.value = currentResolution;
        dropdownResolution.RefreshShownValue();
        dropdownResolution.value = PlayerPrefs.GetInt("numeroDeResolucion", 0);


    }

    public void ChangeResolution(int indexResolution)
    {
        PlayerPrefs.SetInt("numeroDeResolucion", dropdownResolution.value);
        
        Resolution resolution = resolutions[indexResolution];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
