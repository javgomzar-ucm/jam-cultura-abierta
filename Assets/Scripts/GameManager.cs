using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Panel_Color
{
    RED,
    ORANGE,
    YELLOW,
    GREEN,
    WHITE
}

public class GameManager : MonoBehaviour
{
    public List<Transform> kids;
    public List<Panel> panels;
    public Mussona mussona;
    public int score;
    public int red_score;
    public int orange_score;
    public int yellow_score;
    public int green_score;
    public int white_score;
    public int max_score;
    public int min_score;
    public TMP_Text score_text;
    public TMP_Text you_lost_text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = max_score;
        kids = new List<Transform>();
        foreach(GameObject kid in GameObject.FindGameObjectsWithTag("Kid"))
        {
            kids.Add(kid.transform);
        }
        panels = new List<Panel>();
        foreach(GameObject panel_object in GameObject.FindGameObjectsWithTag("Panel"))
        {
            Panel panel = panel_object.GetComponent<Panel>();
            panels.Add(panel);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (kids.Count == 0)
        {
            you_lost_text.enabled = true;
        }
        float min_distance = float.MaxValue;
        foreach (Transform kid in kids)
        {
            float distance = Mathf.Min(min_distance, Vector3.Distance(kid.position, mussona.transform.position));
            if (distance < min_distance)
            {
                min_distance = distance;
                mussona.focused_kid = kid;
            }
        }
        score_text.text = score.ToString();
    }

    private int GetScore(Panel_Color color) => color switch
    {
        Panel_Color.RED => red_score,
        Panel_Color.ORANGE => orange_score,
        Panel_Color.YELLOW => yellow_score,
        Panel_Color.GREEN => green_score,
        Panel_Color.WHITE => white_score,
        _ => 0,
    };

    public void UpdateScore(Panel_Color color)
    {
        score += GetScore(color);
        if (score > max_score)
        {
            score = max_score;
        }
        else if (score < min_score)
        {
            score = min_score;
        }
    }
}
