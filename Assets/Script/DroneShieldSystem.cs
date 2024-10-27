using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneShieldSystem : MonoBehaviour
{
    public int maxShield = 4;
    private int currentShields;
    public GameObject[] shieldObjects;
    //public GameObject droneObject;

    // Start is called before the first frame update
    void Start()
    {
        currentShields = maxShield;
        UpdateShieldVisuals();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateShieldVisuals()
    {
        for (int i = 0; i < shieldObjects.Length; i++)
        {
            //if (i < currentShields)
            //{
            //shieldObjects[i].SetActive(true);
            //}
            //else
            //{
            //shieldObjects[i].SetActive(false);
            //}

            shieldObjects[i].SetActive(i<currentShields);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (currentShields > 0)
            {
                currentShields--;
                UpdateShieldVisuals();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void ResetShield()
    {
        currentShields = maxShield;
        UpdateShieldVisuals();
    }
}
