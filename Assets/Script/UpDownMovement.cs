using UnityEngine;

public class UpDownMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public float range = 100.0f;
    private float startY;

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        float newY = startY + Mathf.PingPong(Time.time * speed, range * 2) - range;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
