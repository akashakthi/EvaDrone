using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    //[SerializeField] public float verticalSpeed;
    //[SerializeField] public float horizontalSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

        //Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        //screenPosition.x = Mathf.Clamp(screenPosition.x, 0.05f, 0.95f);
        //screenPosition.y = Mathf.Clamp(screenPosition.y, 0.05f, 0.95f);

        //Vector3 clampedPosition = Camera.main.ViewportToWorldPoint(screenPosition);

        //clampedPosition.z = transform.position.z;

        //transform.position = clampedPosition;
    }
}
