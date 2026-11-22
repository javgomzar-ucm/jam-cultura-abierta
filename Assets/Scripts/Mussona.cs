using UnityEngine;

public enum State
{
    Following_Player,
    Following_Kid,
}

public class Mussona : MonoBehaviour
{
    public State state;
    public Transform player;
    public Transform focused_kid;
    public float speed;
    public float radius;
    public GameManager manager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.score > 0)
        {
            state = State.Following_Player;
        }
        else
        {
            state = State.Following_Kid;
        }

        Vector3 direction = new Vector3(0, 0, 0);
        if (state == State.Following_Player)
        {
            direction = player.position - transform.position;
        }
        else if (state == State.Following_Kid)
        {
            if (focused_kid != null)
            {
                direction = focused_kid.position - transform.position;
            }
        }

        if (direction.magnitude > radius)
        {
            transform.position = transform.position + speed * Time.deltaTime * direction.normalized;
        }
    }
}
