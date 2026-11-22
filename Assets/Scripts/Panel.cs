using UnityEngine;

public class Panel : MonoBehaviour
{
    public int score;
    public BoxCollider2D box_collider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        box_collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
