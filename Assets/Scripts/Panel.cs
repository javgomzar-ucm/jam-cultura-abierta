using UnityEngine;

public class Panel : MonoBehaviour
{
    public int score;
    public BoxCollider2D box_collider;
    public bool isCursorInside = false;
    public Cursor cursor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        box_collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isCursorInside = true;
        cursor.collided_panel = gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isCursorInside = false;
    }
}
