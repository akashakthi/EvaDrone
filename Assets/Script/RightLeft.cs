using UnityEngine;

public class AutoGerak : MonoBehaviour
{
    public float kecepatan = 2f; // Kecepatan gerakan
    public float batasKiri = -12f; // Batas kiri gerakan
    public float batasKanan = 12f; // Batas kanan gerakan

    private Vector3 arahGerak; // Arah gerakan
    private float posisiAwal; // Posisi awal objek

    void Start()
    {
        arahGerak = Vector3.right; // Mulai bergerak ke kanan
        posisiAwal = transform.position.x; // Menyimpan posisi awal
    }

    void Update()
    {
        // Menghitung posisi baru
        transform.position += arahGerak * kecepatan * Time.deltaTime;

        // Memeriksa apakah objek telah mencapai batas
        if (transform.position.x >= posisiAwal + batasKanan || transform.position.x <= posisiAwal + batasKiri)
        {
            arahGerak = -arahGerak; // Balik arah gerakan
        }
    }
}
