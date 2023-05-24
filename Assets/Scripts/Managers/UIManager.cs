using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image manaBar;
    [SerializeField] private GameObject[] attack1;
    [SerializeField] private GameObject[] attack2;
    [SerializeField] private Image cover1;
    [SerializeField] private Image cover2;


    private PlayerStats playerStats;
    private PlayerCastingMagic playerCastingMagic;
    
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI namePlayer;
    public TextMeshProUGUI namePlayerShadow;
    public GameObject moveInstructions;
    public GameObject attack1Tutorial;
    public GameObject attack2Tutorial;


    private void OnEnable()
    {
        StartCoroutine(MoveInstructions());
    }

    
    private void Update()
    {
        if (GameObject.FindWithTag("Player"))
        {
            playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();

            playerCastingMagic = GameObject.FindWithTag("Player").GetComponent<PlayerCastingMagic>();
        }

        AbilitiesIcon();
        UpdateCoolDown();
       
        namePlayer.text = playerStats.characterStats.Name;
        namePlayerShadow.text = playerStats.characterStats.Name;

        //Barra de vida
        UpdateHealthbar(playerStats.characterMaxHealth, playerStats.characterCurrentHealth);

        //Barra de mana
        UpdateManabar(playerStats.characterMaxMana, playerStats.characterCurrentMana);
    }

    public void UpdateHealthbar(float maxHealth, float health)
    {
        healthBar.fillAmount = health / maxHealth;
        healthText.text = (health*100 / maxHealth).ToString("F0") + "%";
    }

    public void UpdateManabar(float maxMana, float mana)
    {
        manaBar.fillAmount = mana / maxMana;
        manaText.text = (mana*100 / maxMana).ToString("F0") + "%"; 
    }
    
    private void AbilitiesIcon()
    {
        if (playerCastingMagic.spellInfo1.EarthMagic == true)
        {
            attack1[0].SetActive(true);
        }
        if (playerCastingMagic.spellInfo1.FireMagic == true)
        {
            attack1[1].SetActive(true);
        }
        if (playerCastingMagic.spellInfo1.WaterMagic == true)
        {
            attack1[2].SetActive(true);
        }
        if (playerCastingMagic.spellInfo1.WindMagic == true)
        {
            attack1[3].SetActive(true);
        }

        if (playerCastingMagic.spellInfo2.EarthMagic == true)
        {
            attack2[0].SetActive(true);
        }
        if (playerCastingMagic.spellInfo2.FireMagic == true)
        {
            attack2[1].SetActive(true);
        }
        if (playerCastingMagic.spellInfo2.WaterMagic == true)
        {
            attack2[2].SetActive(true);
        }
        if (playerCastingMagic.spellInfo2.WindMagic == true)
        {
            attack2[3].SetActive(true);
        }
    }
    private void UpdateCoolDown()
    {
        cover1.fillAmount = 1 - playerCastingMagic.currentCastTimer1 / playerCastingMagic.attack1CD;

        cover2.fillAmount = 1 - playerCastingMagic.currentCastTimer2 / playerCastingMagic.attack2CD;
    }

    IEnumerator MoveInstructions()
    {
        attack1Tutorial.SetActive(false);
        attack2Tutorial.SetActive(false);
        yield return new WaitForSeconds(5);
        moveInstructions.SetActive(false);
        attack1Tutorial.SetActive(true);
        yield return new WaitForSeconds(5);
        attack1Tutorial.SetActive(false);
        attack2Tutorial.SetActive(true);
        yield return new WaitForSeconds(5);
        attack2Tutorial.SetActive(false);
    }
}
