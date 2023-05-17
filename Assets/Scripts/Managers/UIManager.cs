using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image manaBar;
    private PlayerStats playerStats;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;
    public GameObject playerUI;
    public GameObject combatMap1;
    public GameObject player;

    private void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    private void Update()
    {
        //Barra de vida
        UpdateHealthbar(playerStats.characterMaxHealth, playerStats.characterCurrentHealth);

        //Barra de mana
        UpdateManabar(playerStats.characterMaxMana, playerStats.characterCurrentMana);

    }

    public void UpdateHealthbar(float maxHealth, float health)
    {
        healthBar.fillAmount = health / maxHealth;
        healthText.text = health.ToString("F0") + " / " + maxHealth.ToString();
    }

    public void UpdateManabar(float maxMana, float mana)
    {
        manaBar.fillAmount = mana / maxMana;
        manaText.text = mana.ToString("F0") + " / " + maxMana.ToString();
    }
    
}
