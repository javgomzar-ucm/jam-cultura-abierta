using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float speed;
    private Vector2 move_direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Move(Vector2 direction)
    {
        move_direction = speed * direction.normalized;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(move_direction.x, move_direction.y, 0) * Time.fixedDeltaTime;
    }
}
