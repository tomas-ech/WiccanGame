using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{

    public Animator playerAnimator;
    private PlayerController playerController;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();        
    }

    void Update()
    {
        PlayerMovementAnimations();
        PlayerAbilitiesAnimation();
    }

    private void PlayerMovementAnimations()
    {
        if (playerController.verticalInput > 0)
        {
            playerAnimator.SetBool("IsRunning", true);
        }
        else
        {
            playerAnimator.SetBool("IsRunning", false);
        }

        if (playerController.horizontalInput > 0)
        {
            playerAnimator.SetBool("RightWalk", true);
        }
        else
        {
            playerAnimator.SetBool("RightWalk", false);
        }

        if (playerController.horizontalInput < 0)
        {
            playerAnimator.SetBool("LeftWalk", true);
        }
        else
        {
            playerAnimator.SetBool("LeftWalk", false);
        }

        if (playerController.isOnGround == false)
        {
            playerAnimator.SetBool("IsJumping", true);
        }
        else
        {
            playerAnimator.SetBool("IsJumping", false);
        }
    }

    private void PlayerAbilitiesAnimation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerAnimator.SetBool("BombThrow", true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            playerAnimator.SetBool("BombThrow", false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(RockAttack());
        }
        
    }

    IEnumerator RockAttack()
{
    playerAnimator.SetBool("Rocks", true);
    yield return new WaitForSeconds(1);
    playerAnimator.SetBool("Rocks", false);
}
}


