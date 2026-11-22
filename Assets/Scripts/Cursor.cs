using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Cursor : MonoBehaviour
{
    public float period_seconds = 2f;
    public float amplitude;
    public List<Panel> panels;
    RectTransform rt;
    Vector2 origin;
    public BoxCollider2D box_collider;
    public GameObject collided_panel;
    public GameManager manager;
    
    void Awake()
    {
        rt = (RectTransform)transform;
    }

    void OnEnable()
    {
        origin = rt.anchoredPosition; // capture here so re-enabling doesn't drift
    }

    void OnDisable()
    {
        rt.anchoredPosition = origin; // restore the resting position
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        box_collider = GetComponent<BoxCollider2D>();
        panels = new List<Panel>();
        foreach(GameObject panel_object in GameObject.FindGameObjectsWithTag("Panel"))
        {
            Panel panel = panel_object.GetComponent<Panel>();
            panels.Add(panel);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach(Panel panel in panels)
            {
                ColliderDistance2D d = panel.box_collider.Distance(box_collider);
                if (d.isOverlapped)
                {
                    collided_panel = panel.GameObject();
                    manager.score += panel.score;
                    break;
                }
            }
        }

        float s = Mathf.Sin(2f * Mathf.PI / period_seconds * Time.time);
        rt.anchoredPosition = origin + new Vector2(amplitude * s, 0);
    }
}
