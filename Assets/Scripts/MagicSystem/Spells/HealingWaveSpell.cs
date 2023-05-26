using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HealingWaveSpell : MonoBehaviour
{
    public int bounces;
    public float healing, moveSpeed, rotSpeed, stopDistance, destroytimer;
    public string[] tagsToCheck;

    private Transform target, loopTransform;
    private ParticleSystem impact, loop;
    private bool isDead = false;

    private List<Transform> possibleTargets = new List<Transform>();
    private List<Transform> healedTargets = new List<Transform>();

    void Start()
    {
        impact = transform.Find("Impact").GetComponent<ParticleSystem>();
        loop = transform.Find("Loop").GetComponent<ParticleSystem>();
        loopTransform = loop.transform;

        if(!target && possibleTargets.Count > 0)
        {
            possibleTargets.Remove(target);
            var random = Random.Range(0, possibleTargets.Count);
            target = possibleTargets[random];
        }
        
    }

    void Update()
    {
        if (!isDead)
        {
            if (target)
            {
                if (possibleTargets.Contains(target))
                {
                    possibleTargets.Remove(target);
                }

                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                
                transform.LookAt(target);

                loopTransform.Rotate(Vector3.up, rotSpeed * Time.deltaTime, Space.Self);

                var distance = Vector3.Distance(transform.position, target.position);
                if (distance < stopDistance)
                {
                    Heal();
                }
            }
            else if(possibleTargets.Count > 0)
            {
                possibleTargets.Remove(target);
                var random = Random.Range(0, possibleTargets.Count);
                target = possibleTargets[random];
            }
        }
    }

    private void Heal()
    {

        if (target.CompareTag("Enemy"))
        {
            target.GetComponent<PlayerStats>().characterCurrentHealth += healing;
            healedTargets.Add(target);
            possibleTargets.Remove(target);
            impact.Play();
        }

        if (target.CompareTag("Player"))
        {
            target.GetComponent<PlayerStats>().characterCurrentHealth -= healing;
            healedTargets.Add(target);
            possibleTargets.Remove(target);
            impact.Play();
        }

        if (possibleTargets.Count > 0)
        {
            var random = Random.Range(0, possibleTargets.Count);
            target = possibleTargets[random];
            bounces -= 1;
            if (bounces <= 0) {FinishWave();}
        }
        else {FinishWave();}
    }

    private void FinishWave()
    {
        isDead = true;
        var loopEmi = loop.emission;
        loopEmi.rateOverTime = 0;
        Destroy(gameObject, destroytimer);
    }

    private void OnTriggerStay(Collider other)
    {
        if (tagsToCheck.Contains(other.tag) && !possibleTargets.Contains(other.transform) && target != other.transform && !isDead && !healedTargets.Contains(other.transform))
        {
            possibleTargets.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (tagsToCheck.Contains(other.tag) && !isDead)
        {
            possibleTargets.Remove(other.transform);
        }
    }
}
