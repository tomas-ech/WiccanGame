using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlizzardSpawner : MonoBehaviour
{
    public GameObject flyingObject;
    public int amount;
    public float destroyDelay, spawnInterval, spawnRadius, spawnForce;
    public Vector3 spawnOffset;

    private bool isDead = false;
    private float spawnIntervalTimer;

    void Update()
    {
        if (!isDead)
        {
            if (spawnIntervalTimer <= 0)
            {
                spawnIntervalTimer = spawnInterval;

                amount -= 1;

                var spawnPosition = transform.position + new Vector3(Random.insideUnitCircle.x * spawnRadius, Random.insideUnitCircle.y * spawnRadius) + spawnOffset;

                var obj = Instantiate(flyingObject, spawnPosition, Quaternion.identity);

                obj.transform.SetParent(transform);

                var forceDirection = transform.position - (transform.position + spawnOffset);

                obj.GetComponent<Rigidbody>().AddForce(forceDirection * spawnForce, ForceMode.VelocityChange);

                Destroy(obj, destroyDelay);

                if (amount <= 0)
                {
                    isDead = true;
                    Destroy(gameObject, destroyDelay);
                }
            }
            else {spawnIntervalTimer -= Time.deltaTime;}
        }  
    }
}
