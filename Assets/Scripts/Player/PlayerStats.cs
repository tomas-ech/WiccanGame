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
    [HideInInspector] public bool playerIsDead = false;
    private bool healthRegen;

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
            playerIsDead = true;
            Animator animator = GetComponentInChildren<Animator>();
            animator.SetBool("Dead", true);

            if(this.CompareTag("Enemy"))
            {
                StartCoroutine(DestroyOnDead());
            }
            
        }

        if (healthRegen == true)
        {
            characterCurrentHealth += 100 * Time.deltaTime;
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

        if (characterCurrentHealth < 1)
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
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spell"))
        {
            healthRegen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Spell"))
        {
            healthRegen = false;
        }
    }
}
