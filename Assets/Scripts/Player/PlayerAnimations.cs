using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;
    private PlayerController playerController;
    private PlayerCastingMagic playerMagic;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        playerController = GetComponent<PlayerController>();
        playerMagic = GetComponentInChildren<PlayerCastingMagic>();

    }

    void Update()
    {
        MovementAnimations();

        if (playerMagic.castingMagic1)
        {
            StartCoroutine(CastSpell("castSpell1"));
        }
        else if (playerMagic.castingMagic2)
        {
            StartCoroutine(CastSpell("castSpell2"));
        }
        else if (playerMagic.castingMagic3)
        {
            StartCoroutine(CastSpell("castSpell3"));
        }
        


    }

    private void MovementAnimations()
    {
        anim.SetFloat("xMove", playerController.horizontalInput);
        anim.SetFloat("yMove", playerController.verticalInput);
    }

    IEnumerator CastSpell(string spellName)
    {
        anim.SetBool(spellName, true);
        yield return new WaitForSeconds(1f);
        anim.SetBool(spellName, false);
    }
}
