using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image manaBar;
    [SerializeField] private GameObject[] attack1;
    [SerializeField] private GameObject[] attack2;
    [SerializeField] private GameObject[] defensive;
    [SerializeField] private Image cover1;
    [SerializeField] private Image cover2;
    [SerializeField] private Image cover3;

    [HideInInspector] public bool isMission1;


    private PlayerStats playerStats;
    private PlayerCastingMagic playerCastingMagic;
    
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI namePlayer;
    public TextMeshProUGUI namePlayerShadow;
    public TextMeshProUGUI enemyCountText;
    public GameObject pauseMenu;
    public GameObject moveInstructions;
    public GameObject attack1Tutorial;
    public GameObject attack2Tutorial;
    public GameObject defenseTutorial;
    public GameObject mission1;
    public GameObject tip1;
    public GameObject remainingEnemies;
    
    private int enemyCount;


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

        namePlayer.text = playerStats.characterStats.Name;
        namePlayerShadow.text = playerStats.characterStats.Name;

        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemyCountText.text = enemyCount.ToString();

        PauseGame();
        AbilitiesIcon();
        UpdateCoolDown();
        WinLoseConditions();
        UpdateHealthbar(playerStats.characterMaxHealth, playerStats.characterCurrentHealth);
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

    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);

            if (Time.timeScale == 1 && Cursor.visible == false)
            {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    private void WinLoseConditions()
    {
        if (playerStats.characterCurrentHealth < 1)
        {
            StartCoroutine(PlayerDead());
        }
        if (enemyCount < 1)
        {
            StartCoroutine(PlayerWin());
        }
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

        cover3.fillAmount = 1 - playerCastingMagic.currentCastTimer3 / playerCastingMagic.attack3CD;
    }

    IEnumerator MoveInstructions()
    {
        attack1Tutorial.SetActive(false);
        attack2Tutorial.SetActive(false);
        defenseTutorial.SetActive(false);
        yield return new WaitForSeconds(5);
        moveInstructions.SetActive(false);
        attack1Tutorial.SetActive(true);
        yield return new WaitForSeconds(5);
        attack1Tutorial.SetActive(false);
        attack2Tutorial.SetActive(true);
        yield return new WaitForSeconds(5);
        attack2Tutorial.SetActive(false);
        defenseTutorial.SetActive(true);
        yield return new WaitForSeconds(5);
        defenseTutorial.SetActive(false);
    }

    IEnumerator Mission1()
    {
        attack1Tutorial.SetActive(false);
        attack2Tutorial.SetActive(false);
        defenseTutorial.SetActive(false);
        mission1.SetActive(true);
        yield return new WaitForSeconds(6);
        mission1.SetActive(false);
        yield return new WaitForSeconds(15);
        tip1.SetActive(true);
        yield return new WaitForSeconds(6);
        tip1.SetActive(false);
    }

    IEnumerator PlayerDead()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }

    IEnumerator PlayerWin()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(2);
    }
}
