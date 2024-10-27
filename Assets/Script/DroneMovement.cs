using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    private Rigidbody rb;
    Animator droneAnim;
    //[SerializeField] public float verticalSpeed;
    //[SerializeField] public float horizontalSpeed;
    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        droneAnim = GetComponent<Animator>();
        droneAnim.SetBool("Idle", true);
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f);
        rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);

        bool isMoving = movement != Vector3.zero;

        if (moveHorizontal < 0) // A / Left
        {
            droneAnim.SetBool("Kiri", true);
            droneAnim.SetBool("Idle", false);
            droneAnim.SetBool("Kanan", false);
        }
        else if (moveHorizontal > 0) // D / Right
        {
            droneAnim.SetBool("Kiri", false);
            droneAnim.SetBool("Idle", false);
            droneAnim.SetBool("Kanan", true);
        }
        else if (!isMoving)
        {
            droneAnim.SetBool("Kiri", false);
            droneAnim.SetBool("Idle", true);
            droneAnim.SetBool("Kanan", false);
        }

        //Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        //screenPosition.x = Mathf.Clamp(screenPosition.x, 0.05f, 0.95f);
        //screenPosition.y = Mathf.Clamp(screenPosition.y, 0.05f, 0.95f);

        //Vector3 clampedPosition = Camera.main.ViewportToWorldPoint(screenPosition);

        //clampedPosition.z = transform.position.z;

        //transform.position = clampedPosition;
    }
}
