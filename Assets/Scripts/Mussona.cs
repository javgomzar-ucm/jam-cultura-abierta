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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (state == State.Following_Player)
            {
                state = State.Following_Kid;
            }
            else
            {
                state = State.Following_Player;
            }
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
