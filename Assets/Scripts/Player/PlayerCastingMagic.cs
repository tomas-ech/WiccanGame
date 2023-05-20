using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class PlayerCastingMagic : NetworkBehaviour
{

    [SerializeField] private FireBallSpell spellToCast1;
    [SerializeField] private GameObject prefabSpellCast1;
    //[SerializeField] private Spell spellToCast2;
    //[SerializeField] private Spell spellToCast3;
    [SerializeField] private Transform castPoint;
    [SerializeField] private Transform castPointRotator;
    [SerializeField] private float timeBetweenCast = 0.25f;
    private float currentCastTimer;
    private bool castingMagic;
    private PlayerStats playerStats;


    void Start()
    {
        playerStats = GetComponent<PlayerStats>();        
    }

    void Update()
    {
        bool hasEnoughMana1 = playerStats.characterCurrentMana - spellToCast1.spellInfo.ManaCost >= 0; 

        if(isLocalPlayer)
        {
            if (!castingMagic && hasEnoughMana1 && Input.GetMouseButtonDown(0))
            {
                castingMagic = true;
                playerStats.characterCurrentMana -= spellToCast1.spellInfo.ManaCost;
                currentCastTimer = 0;
                //StartCoroutine(castSpell1());
                CmdCastSpell1();
            }

            if (!castingMagic && hasEnoughMana1 && Input.GetMouseButtonDown(1))
            {
                castingMagic = true;
                playerStats.characterCurrentMana -= spellToCast1.spellInfo.ManaCost;
                //castSpell2();
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
    }

    [Command]
    void CmdCastSpell1()
    {
        GameObject projectile = (GameObject)Instantiate(prefabSpellCast1, castPoint.position, castPointRotator.rotation);
        GameObject owner = this.gameObject;
        NetworkServer.Spawn(projectile, owner);
    }

    /*IEnumerator castSpell1()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(spellToCast1, castPoint.position, castPointRotator.rotation);

    }*/

    /*void castSpell1()
    {
        Instantiate(spellToCast1, castPoint.position, castPointRotator.rotation);
    }*/

    /*void castSpell2()
    {
        Instantiate(spellToCast2, castPoint.position, castPointRotator.rotation);
    }

    void castSpell3()
    {
        Instantiate(spellToCast3, castPoint.position, castPointRotator.rotation);
    }*/
}
