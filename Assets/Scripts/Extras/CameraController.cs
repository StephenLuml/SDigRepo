using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Range(0f, 1f),SerializeField]
    private float smoothing;
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Transform player;

    private void FixedUpdate()
    {
        Vector3 followPos = player.position;
        followPos.z = transform.position.z;

        transform.position = followPos;
    }
}
