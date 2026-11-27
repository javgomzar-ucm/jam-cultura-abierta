using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuPausa : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject menuPausa;
    public GameObject opcionesMenu;

    [Header("UI Pausa (Opcional)")]
    public TMP_Text scorePausaText;

    [Header("Referencias del jugador")]
    public PlayerMovement playerMovement;

    [Header("Sliders de audio")]
    public Slider AmbienceSlider;
    public Slider MusicSlider;
    public Slider FXSlider;

    private bool juegoPausado = false;

    void Start()
    {
        if (menuPausa != null) menuPausa.SetActive(false);
        if (opcionesMenu != null) opcionesMenu.SetActive(false);

        juegoPausado = false;

        if (SoundManager.Instance != null)
        {
            if (AmbienceSlider != null) AmbienceSlider.value = PlayerPrefs.GetFloat("AmbienceVolume", 1f);
            if (MusicSlider != null) MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
            if (FXSlider != null) FXSlider.value = PlayerPrefs.GetFloat("FXVolume", 1f);
        }

        OnMusicVolumeChanged();
        OnAmbienceVolumeChanged();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Reanudar()
    {
        juegoPausado = false;
        if (menuPausa != null) menuPausa.SetActive(false);
        if (opcionesMenu != null) opcionesMenu.SetActive(false);
        GameManager.SetPaused(false);
    }

    public void Pausar()
    {
        juegoPausado = true;
        if (menuPausa != null) menuPausa.SetActive(true);
        if (opcionesMenu != null) opcionesMenu.SetActive(false);

        if (scorePausaText != null && GameManager.Instance != null)
        {
            scorePausaText.text = GameManager.Instance.score.ToString();
        }

        GameManager.SetPaused(true);
    }

    public void MostrarOpciones()
    {
        if (menuPausa != null) menuPausa.SetActive(false);
        if (opcionesMenu != null) opcionesMenu.SetActive(true);
    }

    public void VolverMenuPausa()
    {
        if (opcionesMenu != null) opcionesMenu.SetActive(false);
        if (menuPausa != null) menuPausa.SetActive(true);
    }

    public void CargarMenuPrincipal()
    {
        juegoPausado = false;
        GameManager.ResetGameState();
        SceneManager.LoadScene(Escenas.MainMenu);
    }

    public void Salir()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    public void OnAmbienceVolumeChanged()
    {
        if (AmbienceSlider == null) return;
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SetAmbienceVolume(AmbienceSlider.value);
        }
        PlayerPrefs.SetFloat("AmbienceVolume", AmbienceSlider.value);
    }

    public void OnMusicVolumeChanged()
    {
        if (MusicSlider == null) return;
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SetMusicVolume(MusicSlider.value);
        }
        PlayerPrefs.SetFloat("MusicVolume", MusicSlider.value);
    }

    public void OnFXVolumeChanged()
    {
        if (FXSlider == null) return;
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SetFxVolume(FXSlider.value);
        }
        PlayerPrefs.SetFloat("FXVolume", FXSlider.value);
    }

    public int GetCurrentScore()
    {
        return GameManager.Instance != null ? GameManager.Instance.score : 0;
    }
}