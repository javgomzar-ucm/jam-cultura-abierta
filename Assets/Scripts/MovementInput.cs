using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class MovementInput : MonoBehaviour
{
    public MovementController controller;
    public GameManager manager;
    public Cursor cursor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void OnMove(CallbackContext ctx)
    {
        if (ctx.performed)
        {
            controller.Move(ctx.ReadValue<Vector2>());
        }
        else {
            controller.Move(Vector2.zero);
        }
    }

    public void Interact(CallbackContext ctx)
    {
        if (ctx.started)
        {
            manager.UpdateScore(cursor.collided_panel.color);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
