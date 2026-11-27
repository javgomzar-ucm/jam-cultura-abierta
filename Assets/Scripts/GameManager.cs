using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static bool IsPaused { get; private set; } = false;

    [Header("Gameplay")]
    public List<Transform> kids;
    public Mussona mussona;
    public int score;
    public TMP_Text score_text;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        IsPaused = false;
        Time.timeScale = 1f;
    }

    private void Start()
    {
        kids = new List<Transform>();
        foreach (GameObject kid in GameObject.FindGameObjectsWithTag("Kid"))
        {
            kids.Add(kid.transform);
        }

        UpdateScoreUI();
    }

    private void Update()
    {
        if (IsPaused) return;

        UpdateFocusedKid();
        UpdateScoreUI();
    }

    private void UpdateFocusedKid()
    {
        if (mussona == null || kids == null || kids.Count == 0) return;

        float minDistance = float.MaxValue;
        Transform closestKid = null;

        foreach (Transform kid in kids)
        {
            if (kid == null) continue;

            float distance = Vector3.Distance(kid.position, mussona.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestKid = kid;
            }
        }

        if (closestKid != null)
        {
            mussona.focused_kid = closestKid;
        }
    }

    private void UpdateScoreUI()
    {
        if (score_text != null)
        {
            score_text.text = score.ToString();
        }
    }

    public static void SetPaused(bool paused)
    {
        IsPaused = paused;
        Time.timeScale = paused ? 0f : 1f;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
    }

    public static void ResetGameState()
    {
        IsPaused = false;
        Time.timeScale = 1f;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;

        if (Instance != null)
        {
            Instance.score = 0;
            Instance.UpdateScoreUI();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }
}