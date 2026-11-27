using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Loading : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI progressText;
    public Slider progressBar;
    public TextMeshProUGUI continueText;

    private AsyncOperation _operation;

    private void Start()
    {
        Time.timeScale = 1f;

        if (continueText != null)
        {
            continueText.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForEndOfFrame();

        string sceneToLoad = Escenas.Tutorial;

        if (GameSettings.CurrentGameMode == "Normal")
        {
            sceneToLoad = Escenas.Normal;
        }
        else if (GameSettings.CurrentGameMode == "Pruebas" || GameSettings.CurrentGameMode == "Tutorial")
        {
            sceneToLoad = Escenas.Tutorial;
        }

        _operation = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);
        _operation.allowSceneActivation = false;

        while (!_operation.isDone)
        {
            float progress = Mathf.Clamp01(_operation.progress / 0.9f);

            if (progressBar != null)
            {
                progressBar.value = progress;
            }

            if (progressText != null)
            {
                progressText.text = (progress * 100).ToString("0.0") + "%";
            }

            if (progress >= 1f && continueText != null)
            {
                continueText.gameObject.SetActive(true);
            }

            if (progress >= 1f && Input.GetKeyDown(KeyCode.Space))
            {
                FinishLoading();
            }

            yield return null;
        }
    }

    private void FinishLoading()
    {
        if (_operation != null)
        {
            _operation.allowSceneActivation = true;
        }
    }
}