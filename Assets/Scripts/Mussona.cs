using Unity.VisualScripting;
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
    public float radius;
    public float pct_follow_player;
    public GameManager manager;
    public MovementController controller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(0, 0, 0);
        if (manager.score >= 0)
        {
            state = State.Following_Player;
            direction = player.position - transform.position;
        }
        else
        {
            state = State.Following_Kid;
            if (focused_kid != null)
            {
                direction = focused_kid.position - transform.position;
            }
        }

        if (direction.magnitude > radius)
        {
            controller.Move(direction);
        }
        else
        {
            if (state == State.Following_Kid && focused_kid != null)
            {
                manager.kids.Remove(focused_kid.transform);
                Destroy(focused_kid.GameObject());
            }
            controller.Move(Vector3.zero);
        }
    }
}
