using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject audioPanel;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFXSlider;

    [SerializeField] AudioMixer mixer;
    [SerializeField] string exposedMusicParamName;
    [SerializeField] string exposedSFXParamName;



    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }


    public void ToggleAudio()
    {
        audioPanel.SetActive(!audioPanel.activeSelf);
    }


    public void SetMusicVolume()
    {
        float volumeDB = Mathf.Log10(Mathf.Max(musicSlider.value, 0.00001f))*20f;
        mixer.SetFloat(exposedMusicParamName, volumeDB);
    }

    public void SetSFXVolume()
    {
        // float volumeDB = Mathf.Log10(Mathf.Max(SFXSlider.value, 0.00001f))*20f;
        // mixer.SetFloat(exposedSFXParamName, volumeDB);
    }
}
