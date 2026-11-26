using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject MainCanvas, PlayMenuCanvas, OptionsMenuCanvas;
    [SerializeField] private Slider AmbienceSlider, MusicSlider, FXSlider;

    void Start()
    {
        MostrarPanelMenuPrincipal();

        if (SoundManager.Instance != null)
        {
            AmbienceSlider.value = PlayerPrefs.GetFloat("AmbienceVolume", 1f);
            MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
            FXSlider.value = PlayerPrefs.GetFloat("FXVolume", 1f);
        }

        OnMusicVolumeChanged();
        OnAmbienceVolumeChanged();
    }

    public void MostrarPanelMenuPrincipal()
    {
        MainCanvas.SetActive(true);
        PlayMenuCanvas.SetActive(false);
        OptionsMenuCanvas.SetActive(false);
    }

    public void MostrarPlayMenu()
    {
        MainCanvas.SetActive(false);
        PlayMenuCanvas.SetActive(true);
        OptionsMenuCanvas.SetActive(false);
    }

    public void MostrarPanelAjustes()
    {
        MainCanvas.SetActive(false);
        PlayMenuCanvas.SetActive(false);
        OptionsMenuCanvas.SetActive(true);
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Aquí se cierra");
    }

    public void IniciarTutorial()
    {
        GameSettings.CurrentGameMode = "Tutorial";
        SceneManager.LoadScene("Loading");
    }


    public void IniciarNormal()
    {
        GameSettings.CurrentGameMode = "Normal";
        SceneManager.LoadScene("Loading");
    }

    public void OnAmbienceVolumeChanged()
    {
        SoundManager.Instance.SetAmbienceVolume(AmbienceSlider.value);
        PlayerPrefs.SetFloat("AmbienceVolume", AmbienceSlider.value);
    }

    public void OnMusicVolumeChanged()
    {
        SoundManager.Instance.SetMusicVolume(MusicSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", MusicSlider.value);
    }

    public void OnFXVolumeChanged()
    {
        SoundManager.Instance.SetFxVolume(FXSlider.value);
        PlayerPrefs.SetFloat("FXVolume", FXSlider.value);
    }
}
