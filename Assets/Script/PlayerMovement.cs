using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Kecepatan gerak pemain

    void Update()
    {
        // Mengambil input dari keyboard
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        // Mengatur gerakan berdasarkan input
        if (Input.GetKey(KeyCode.A)) // Kiri
        {
            moveHorizontal = -1f;
        }
        if (Input.GetKey(KeyCode.D)) // Kanan
        {
            moveHorizontal = 1f;
        }
        if (Input.GetKey(KeyCode.W)) // Atas
        {
            moveVertical = 1f;
        }
        if (Input.GetKey(KeyCode.S)) // Bawah
        {
            moveVertical = -1f;
        }

        // Menghitung gerakan
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f); // Z = 0 untuk gerakan 2D

        // Menerapkan gerakan pada pemain
        transform.position += movement * moveSpeed * Time.deltaTime;
    }
}
