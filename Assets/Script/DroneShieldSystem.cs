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
        UpdateShieoldVisuals();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateShieoldVisuals()
    {
        for (int i = 0; i < shieldObjects.Length; i++)
        {
            if (i < currentShields)
            {
                shieldObjects[i].SetActive(true);
            }
            else
            {
                shieldObjects[i].SetActive(false);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (currentShields > 0)
            {
                currentShields--;
                UpdateShieoldVisuals();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
