using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Transform> kids;
    public Mussona mussona;
    public int score;
    public TMP_Text score_text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        kids = new List<Transform>();
        foreach(GameObject kid in GameObject.FindGameObjectsWithTag("Kid"))
        {
            kids.Add(kid.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
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
}
