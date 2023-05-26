using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WispSpell : MonoBehaviour
{
    public string[] tagsToCheck;
    public SpellScriptableObject spellInfo;
    public int flickerAmount = 5;
    public float movementSpeed = 0.5f;
    public float rotationSpeed = 5f;
    public float stopRange = 1.5f;
    public float flickerDuration = 2;
    public float flickerRestDuration = 3;
    public float idleFloatDistance = 0.5f;

    private float radius;
    private bool flickering = false, isAlive = true;
    private float flickerDurationCounter, flickerRestDurationCounter;
    private ParticleSystem particle;
    private Transform wisp;
    private Vector3 upPos, downPos;

    void Start()
    {
        wisp = transform.Find("Wisp");

        upPos = wisp.position;
        downPos = wisp.position;
        upPos.y += idleFloatDistance;
        downPos.y -= idleFloatDistance;

        particle = transform.Find("Wisp/Effect").GetComponent<ParticleSystem>();

        flickerDurationCounter = flickerDuration;
        flickerRestDurationCounter = flickerRestDuration;  

        radius = spellInfo.SpellRadius; 
    }

    void Update()
    {
        if (isAlive == true)
        {
            wisp.position = Vector3.Lerp(upPos, downPos, (Mathf.Sin(1 * Time.deltaTime) + 1f) / 2f);

            if ( flickerAmount > 0)
            {
                if (flickering)
                {
                    Flicker(transform.position);
                }
                else
                {
                    if (flickerRestDurationCounter > 0)
                    {
                        flickerRestDurationCounter -= Time.deltaTime;
                    }
                    else
                    {
                        var emi = particle.emission;
                        emi.rateOverTime = 20;

                        flickering = true;
                        flickerDurationCounter = flickerDuration;
                    }
                }
            }
            else {isAlive = false;}
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Flicker(Vector3 destination)
    {
        if ( flickerDurationCounter > 0)
        {
            flickerDurationCounter -= Time.deltaTime;
            flickering = true;
            if (!AudioManager.Instance.audioManager.isPlaying){AudioManager.Instance.audioManager.PlayOneShot(AudioManager.Instance.radiationWisp);}

            Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, radius);

            foreach(Collider col in enemiesInRange)
            {
                if (tagsToCheck.Contains(col.tag))
                {
                    //Para el movimiento
                    var distance = Vector3.Distance(col.transform.position, transform.position);
                    PlayerStats playerStats = col.GetComponent<PlayerStats>();
                    playerStats.characterCurrentHealth -= spellInfo.DamageAmount;

                    if (distance > stopRange)
                    {
                        col.transform.position = Vector3.MoveTowards(col.transform.position, destination, movementSpeed * Time.deltaTime);
                    }

                    //Para la rotacion
                    Vector3 dir = transform.position - col.transform.position;
                    dir.y = 0;

                    Quaternion rot = Quaternion.LookRotation(dir);
                    col.transform.rotation = Quaternion.Slerp(col.transform.rotation, rot, rotationSpeed * Time.deltaTime);
                }
            }
        }
        else
        {
            var emi = particle.emission;
            emi.rateOverTime = 0;

            flickering = false;
            flickerRestDurationCounter = flickerRestDuration;
            flickerAmount -= 1;
        }

    }

}
