using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GelidonAbilities : MonoBehaviour
{
  [SerializeField] private GameObject spellToCast1;
    [SerializeField] private SpellScriptableObject spellInfo1;
    [SerializeField] private GameObject spellToCast2;
    [SerializeField] private SpellScriptableObject spellInfo2;
    [SerializeField] private Transform castPoint;
    [SerializeField] private Transform groundPoint;
    [SerializeField] private Transform castPointRotator;
    [SerializeField] private float timeBetweenCast = 1f;
    private float currentCastTimer;
    private bool castingMagic;
    private PlayerStats playerStats;


    void Start()
    {
        playerStats = GetComponent<PlayerStats>();        
    }

    void Update()
    {
        bool hasEnoughMana1 = playerStats.characterCurrentMana - spellInfo1.ManaCost >= 0;
        bool hasEnoughMana2 = playerStats.characterCurrentMana - spellInfo2.ManaCost >= 0; 

        if (!castingMagic && hasEnoughMana1 && Input.GetMouseButtonDown(0))
        {
            castingMagic = true;
            playerStats.characterCurrentMana -= spellInfo1.ManaCost;
            currentCastTimer = 0;
            StartCoroutine(castSpell1());
        }

        if (!castingMagic && hasEnoughMana2 && Input.GetMouseButtonDown(1))
        {
            castingMagic = true;
            playerStats.characterCurrentMana -= spellInfo2.ManaCost;
            currentCastTimer = 0;
            StartCoroutine(castSpell2());
        }

        if (castingMagic)
        {
            currentCastTimer += Time.deltaTime;

            if (currentCastTimer > timeBetweenCast)
            {
                castingMagic = false;
            }
        }
    }

    IEnumerator castSpell1()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(spellToCast1, castPoint.position + new Vector3(-1f, 0f, 0f), castPointRotator.rotation);
        yield return new WaitForSeconds(0.3f);
        Instantiate(spellToCast1, castPoint.position + new Vector3(1.5f, 0f, 0f), castPointRotator.rotation);
        yield return new WaitForSeconds(0.3f);
        Instantiate(spellToCast1, castPoint.position + new Vector3(-1f, 0f, 0f), castPointRotator.rotation);
        yield return new WaitForSeconds(0.3f);
        Instantiate(spellToCast1, castPoint.position + new Vector3(1.5f, 0f, 0f), castPointRotator.rotation);

    }
    IEnumerator castSpell2()
    {
        yield return new WaitForSeconds(1.5f);
        Instantiate(spellToCast2, groundPoint.position + new Vector3(0f, 10f, 10f), castPointRotator.rotation);
    }
}
