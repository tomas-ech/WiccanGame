using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Character Stats")]
    public PlayerScriptableObject characterStats;
    public float characterMaxHealth;
    public float characterCurrentHealth;
    public float characterMaxMana;
    public float characterCurrentMana;
    public float characterManaRecharge;
    public float characterSpeed;
    [HideInInspector] public float timeToRechargeMana = 5f;
    [HideInInspector] public float currentManaRechargeTimer; 
    [HideInInspector] public bool playerIsDead;

    void Start()
    {
        characterMaxHealth = characterStats.Health;
        characterCurrentHealth = characterMaxHealth;

        characterMaxMana = characterStats.Mana;
        characterCurrentMana = characterMaxMana;

        characterManaRecharge = characterStats.Spirit / 3;

        characterSpeed = characterStats.Speed;
    }

    void Update()
    {
        StatsControl();

        if (characterCurrentHealth < 1)
        {
            Animator animator = GetComponentInChildren<Animator>();
            animator.SetBool("Dead", true);
            StartCoroutine(DestroyOnDead());
        }
    }

    //Control del mana y la vida maximos y minimos
    public void StatsControl()
    {
        //Control de la vida
        if (characterCurrentHealth > characterMaxHealth)
        {
            characterCurrentHealth = characterMaxHealth;
        }

        if (characterCurrentHealth < 0)
        {
            characterCurrentHealth = 0;
        }

        //Control del mana
        if (characterCurrentMana < characterMaxMana )
        {
            currentManaRechargeTimer += Time.deltaTime;

            if(currentManaRechargeTimer > timeToRechargeMana)
            {
                characterCurrentMana += characterManaRecharge * Time.deltaTime;
               
                if (characterCurrentMana > characterMaxMana)
                {
                    characterCurrentMana = characterMaxMana;
                }
            }
        }
        
        if (characterCurrentMana < 0)
        {
            characterCurrentMana = 0;
        }
    }

    IEnumerator DestroyOnDead()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
