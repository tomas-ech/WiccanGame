using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TMP_Text playerText;
    private PlayerController playerController;

    public void SetPlayer(PlayerController playerController)
    {
        this.playerController = playerController;
        playerText.text = "Stiven";
    }
}
