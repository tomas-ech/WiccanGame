using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour
{
    private PlayerStats healthComponent;
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            healthComponent = other.GetComponent<PlayerStats>();
            InvokeRepeating("LavaDamage", 0f, 2f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CancelInvoke();
    }

    private void LavaDamage()
    {
        healthComponent.characterCurrentHealth -= 250; 
    }
}
