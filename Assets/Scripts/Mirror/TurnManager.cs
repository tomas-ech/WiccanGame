using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TurnManager : MonoBehaviour
{
    private List<PlayerController> players = new List<PlayerController>();

    public void AddPlayer(PlayerController playerController)
    {
        players.Add(playerController);
    }
}
