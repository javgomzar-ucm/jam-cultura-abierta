using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S)) {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D)) {
            direction += Vector2.right;
        }

        transform.position = transform.position + speed * Time.deltaTime * new Vector3(direction.x, direction.y, 0);
    }
}
