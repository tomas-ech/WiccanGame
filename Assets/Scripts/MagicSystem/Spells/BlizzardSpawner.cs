using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlizzardSpawner : MonoBehaviour
{
    public GameObject flyingObject;
    private GameObject player;
    public int amount;
    public float destroyDelay, spawnInterval, spawnRadius, spawnForce;
    public Vector3 spawnOffset;

    private bool isDead = false;
    private float spawnIntervalTimer;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        transform.rotation = player.transform.rotation;
    }

    void Update()
    {
        

        if (!isDead)
        {
            if (spawnIntervalTimer <= 0)
            {
                spawnIntervalTimer = spawnInterval;

                amount -= 1;

                var spawnPosition = transform.position + new Vector3(Random.insideUnitCircle.x * spawnRadius, 0, Random.insideUnitCircle.y * spawnRadius) + spawnOffset;

                var obj = Instantiate(flyingObject, spawnPosition, player.transform.rotation);

                obj.transform.SetParent(transform);

                var forceDirection = -transform.up;

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
