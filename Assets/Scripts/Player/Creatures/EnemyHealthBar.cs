using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image enemyHealthBar;
    private PlayerStats enemyStats;

    void Start()
    {
        enemyStats = GetComponentInParent<PlayerStats>();
    }

    void Update()
    {
        enemyHealthBar.fillAmount = enemyStats.characterCurrentHealth / enemyStats.characterMaxHealth;

        transform.LookAt(GameObject.FindWithTag("Player").transform.position);

        if (enemyStats.characterCurrentHealth < 1) {Destroy(gameObject);}
    }
}
