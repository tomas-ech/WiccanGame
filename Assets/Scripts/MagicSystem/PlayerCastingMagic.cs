using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCastingMagic : MonoBehaviour
{
    [SerializeField] private GameObject spellToCast1;
    public SpellScriptableObject spellInfo1;
    [SerializeField] private GameObject spellToCast2;
    public SpellScriptableObject spellInfo2;
    [SerializeField] private GameObject spellToCast3;
    public SpellScriptableObject spellInfo3;
    [SerializeField] private Transform castPoint;
    [SerializeField] private Transform groundPoint;
    [SerializeField] private Transform castPointRotator;
    public float attack1CD = 1f;
    public float attack2CD = 6f;
    public float attack3CD = 10f;
    [HideInInspector] public float currentCastTimer1;
    [HideInInspector] public float currentCastTimer2;
    [HideInInspector] public float currentCastTimer3;
    [HideInInspector] public bool castingMagic1;
    [HideInInspector] public bool castingMagic2;
    [HideInInspector] public bool castingMagic3;
    private PlayerStats playerStats;
    private string characterName;


    void Start()
    {
        playerStats = GetComponent<PlayerStats>();   
        characterName = playerStats.characterStats.Name;
    }

    void Update()
    {
        bool hasEnoughMana1 = playerStats.characterCurrentMana - spellInfo1.ManaCost >= 0;
        bool hasEnoughMana2 = playerStats.characterCurrentMana - spellInfo2.ManaCost >= 0; 
        bool hasEnoughMana3 = playerStats.characterCurrentMana - spellInfo3.ManaCost >= 0; 

        if (!castingMagic1 && hasEnoughMana1 && Input.GetMouseButtonDown(0))
        {
            castingMagic1 = true;
            currentCastTimer1 = 0;
            playerStats.characterCurrentMana -= spellInfo1.ManaCost;
            playerStats.currentManaRechargeTimer = 0;
            

            if(characterName == "Thazarian"){StartCoroutine(Thazarian1());}
            if(characterName == "Gelidon"){StartCoroutine(Gelidon1());}
        }

        if (castingMagic1)
        {
            currentCastTimer1 += Time.deltaTime;

            if (currentCastTimer1 > attack1CD)
            {
                castingMagic1 = false;
            }
        }

        if (!castingMagic2 && hasEnoughMana2 && Input.GetMouseButtonDown(1))
        {
            castingMagic2 = true;
            currentCastTimer2 = 0;

            playerStats.characterCurrentMana -= spellInfo2.ManaCost;
            playerStats.currentManaRechargeTimer = 0;
            
            if(characterName == "Thazarian"){StartCoroutine(Thazarian2());}
            if(characterName == "Gelidon"){StartCoroutine(Gelidon2());}
        }

        if (castingMagic2)
        {
            currentCastTimer2 += Time.deltaTime;

            if (currentCastTimer2 > attack2CD)
            {
                castingMagic2 = false;
            }
        }

        if (!castingMagic3 && hasEnoughMana3 && Input.GetKeyDown(KeyCode.Tab))
        {
            castingMagic3 = true;
            currentCastTimer3 = 0;

            playerStats.characterCurrentMana -= spellInfo3.ManaCost;
            playerStats.currentManaRechargeTimer = 0;
            
            if(characterName == "Thazarian"){StartCoroutine(Defensive1());}
            if(characterName == "Gelidon"){StartCoroutine(Defensive1());}
        }
        
        if (castingMagic3)
        {
            currentCastTimer3 += Time.deltaTime;

            if (currentCastTimer3 > attack3CD)
            {
                castingMagic3 = false;
            }
        }
    }


    IEnumerator Thazarian1()
    {
        yield return new WaitForSeconds(0.5f);
        AudioManager.Instance.audioManager.PlayOneShot(AudioManager.Instance.throwFire);
        Instantiate(spellToCast1, castPoint.position, castPointRotator.rotation);

    }
    IEnumerator Thazarian2()
    {
        yield return new WaitForSeconds(1.5f);
        AudioManager.Instance.audioManager.PlayOneShot(AudioManager.Instance.rockAttack);
        Instantiate(spellToCast2, groundPoint.position, castPointRotator.rotation);
    }

    IEnumerator Gelidon1()
    {
        yield return new WaitForSeconds(0.5f);
        AudioManager.Instance.audioManager.PlayOneShot(AudioManager.Instance.windPunch);
        Instantiate(spellToCast1, castPoint.position + new Vector3(-1f, 0f, 0f), groundPoint.rotation);
        yield return new WaitForSeconds(0.3f);
        Instantiate(spellToCast1, castPoint.position + new Vector3(1f, 0f, 0f), groundPoint.rotation);
        AudioManager.Instance.audioManager.PlayOneShot(AudioManager.Instance.windPunch);
        yield return new WaitForSeconds(0.3f);
        Instantiate(spellToCast1, castPoint.position + new Vector3(-1f, 0f, 0f), castPointRotator.rotation);
        AudioManager.Instance.audioManager.PlayOneShot(AudioManager.Instance.windPunch);
        yield return new WaitForSeconds(0.3f);
        Instantiate(spellToCast1, castPoint.position + new Vector3(1f, 0f, 0f), castPointRotator.rotation);
        AudioManager.Instance.audioManager.PlayOneShot(AudioManager.Instance.windPunch);

    }
    IEnumerator Gelidon2()
    {
        yield return new WaitForSeconds(1.5f);
        AudioManager.Instance.audioManager.PlayOneShot(AudioManager.Instance.blizzard);
        Instantiate(spellToCast2, transform.position + new Vector3(0f, 10f, 0f), castPointRotator.localRotation);
    }

     IEnumerator Defensive1()
    {
        yield return new WaitForSeconds(0.1f);
        Instantiate(spellToCast3, groundPoint.parent.parent.transform.position, castPointRotator.rotation);
    }
}
