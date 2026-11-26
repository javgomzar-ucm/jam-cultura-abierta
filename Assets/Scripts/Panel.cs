using UnityEngine;

public class Panel : MonoBehaviour
{
    public BoxCollider2D box_collider;
    public Panel_Color color;
    public bool isCursorInside;
    public Cursor cursor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        box_collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isCursorInside = true;
        cursor.collided_panel = this;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isCursorInside = false;
        if (color == Panel_Color.WHITE && (cursor.manager.score > cursor.manager.min_score))
        {
            cursor.manager.score -= 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
