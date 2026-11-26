using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.InputSystem.InputAction;
using Unity.VisualScripting;

public class Cursor : MonoBehaviour
{
    public float period_seconds = 2f;
    public float amplitude;
    private float phase = 0;
    RectTransform rt;
    Vector2 origin;
    public BoxCollider2D box_collider;
    public Panel collided_panel;
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
    }

    void Update()
    {
        phase += 2f * Mathf.PI / period_seconds * Time.deltaTime;
        rt.anchoredPosition = origin + new Vector2(amplitude * Mathf.Sin(phase), 0);
    }
}
