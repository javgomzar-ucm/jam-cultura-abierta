using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private float cameraX;

    void Start()
    {
        cameraX = transform.position.x;
    }

    void Update()
    {
        transform.position = new Vector3(
            cameraX,
            player.position.y,
            transform.position.z
        );
    }
}
