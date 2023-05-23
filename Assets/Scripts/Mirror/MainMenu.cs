using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;
using System;
using System.Security.Cryptography;
using System.Text;

[System.Serializable]
public class Match : NetworkBehaviour
{
    public string idRoom;
    public readonly List<GameObject> players = new List<GameObject>();

    public Match(string idRoom, GameObject player)
    {
        this.idRoom = idRoom;
        players.Add(player);
    }
}

public class MainMenu : NetworkBehaviour
{
    public static MainMenu sharedInstance;
    public readonly SyncList<Match> matches = new SyncList<Match>();
    public readonly SyncList<string> matchIDs = new SyncList<string>();
    public TMP_InputField joinInput;
    public Button hostButton;
    public Button joinButton;
    public Canvas lobbyCanvas;

    public Transform uiPlayerParent;
    public GameObject uiPlayerPrefab;
    public TMP_Text idText;
    public Button beginGameButton;
    public GameObject turnManager;
    public bool inGame;

    private void Start()
    {
        sharedInstance = this;
    }

    private void Update()
    {
        if (!inGame)
        {
            PlayerController[] players = FindObjectsOfType<PlayerController>();

            for (int i = 0; i < players.Length; i++)
            {
                players[i].gameObject.transform.localScale = Vector3.zero;
            }
        }
    }

    public void Host()
    {
        joinInput.interactable = false;
        hostButton.interactable = false;
        joinButton.interactable = false;

        PlayerController.localPlayer.HostGame();
    }

    public void HostSuccess(bool success, string matchId)
    {
        if(success)
        {
            lobbyCanvas.enabled = true;

            SpawnPlayerUIPrefab(PlayerController.localPlayer);
            idText.text = matchId;
            beginGameButton.interactable = true;
        }
        else
        {
            joinInput.interactable = true;
            hostButton.interactable = true;
            joinButton.interactable = true;
        }
    }

    public void Join()
    {
        joinInput.interactable = false;
        hostButton.interactable = false;
        joinButton.interactable = false;

        PlayerController.localPlayer.JoinGame(joinInput.text.ToUpper());
    }

    public void JoinSuccess(bool success, string matchId)
    {
        if (success)
        {
            lobbyCanvas.enabled = true;

            SpawnPlayerUIPrefab(PlayerController.localPlayer);
            idText.text = matchId;
            beginGameButton.interactable = false;
        }
        else
        {
            joinInput.interactable = true;
            hostButton.interactable = true;
            joinButton.interactable = true;
        }
    }

    public bool HostGame(string matchId, GameObject player)
    {
        if(!matchIDs.Contains(matchId))
        {
            matchIDs.Add(matchId);
            matches.Add(new Match(matchId, player));
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool JoinGame(string matchId, GameObject player)
    {
        if(matchIDs.Contains(matchId))
        {
            for (int i = 0; i < matches.Count; i++)
            {
                if(matches[i].idRoom == matchId)
                {
                    matches[i].players.Add(player);
                    break;
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public static string GetRandomId()
    {
        string id = string.Empty;

        for (int i = 0; i < 2; i++)
        {
            int rand = UnityEngine.Random.Range(0, 36);
            if(rand < 26)
            {
                id += (char)(rand + 65);
            }
            else
            {
                id += (rand - 26).ToString();
            }
        }

        return id;
    }

    public void SpawnPlayerUIPrefab(PlayerController playerController)
    {
        GameObject newUIPlayer = Instantiate(uiPlayerPrefab, uiPlayerParent);
        newUIPlayer.GetComponent<PlayerUI>().SetPlayer(playerController);
    }

    public void StartGame()
    {
        PlayerController.localPlayer.BeginGame();
    }

    public void BeginGame(string matchId)
    {
        GameObject newTurnManager = Instantiate(turnManager);
        NetworkServer.Spawn(newTurnManager);
        newTurnManager.GetComponent<NetworkMatch>().matchId = matchId.ToGuid();
        TurnManager auxTurnManager = newTurnManager.GetComponent<TurnManager>();

        for (int i = 0; i < matches.Count; i++)
        {
            if(matches[i].idRoom == matchId)
            {
                foreach (var player in matches[i].players)
                {
                    PlayerController auxPlayer = player.GetComponent<PlayerController>();
                    auxTurnManager.AddPlayer(auxPlayer);
                    auxPlayer.StartGame(); 
                }
                break;
            }
        }
    }
}

public static class MatchExtension
{
    public static Guid ToGuid(this string id)
    {
        MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
        byte[] inputBytes = Encoding.Default.GetBytes(id);
        byte[] hasBytes = provider.ComputeHash(inputBytes);

        return new Guid(hasBytes);
    }
}
