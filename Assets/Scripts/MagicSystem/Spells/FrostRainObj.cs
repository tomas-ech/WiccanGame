using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FrostRainObj : MonoBehaviour
{
    public SpellScriptableObject spellInfo;
    public string[] tagsToCheck;
    public float impactRadius;
    public float destroyDelay;

    private GameObject impactFx;

    void Start()
    {
        impactFx = transform.Find("ImpactFx").gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            PlayerStats healthComponent = other.GetComponent<PlayerStats>();
            healthComponent.characterCurrentHealth -= spellInfo.DamageAmount;   
            Debug.Log("Golpeado con hielitos!");
        }

        if (tagsToCheck.Contains(other.tag))
        {
            Collider[] objectsInRange = Physics.OverlapSphere(transform.position, impactRadius);

            foreach(Collider col in objectsInRange)
            {
                Rigidbody enemy = col.GetComponent<Rigidbody>();

                if (enemy != null)
                {
                    Destroy(gameObject);
                }
            }

            impactFx.SetActive(true);
            impactFx.transform.SetParent(null);
            Destroy(impactFx, destroyDelay);
            Destroy(gameObject);
        }

    }
}
