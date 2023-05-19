using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsScript : MonoBehaviour
{
    private SpawnManager spawnMatch;
    public GameObject UiManager;
    public GameObject playerUI;
    //public GameObject combatMap1;
    //public GameObject player;
    //public GameObject boardCamera;
    //public GameObject mainCamera;
    public GameObject initialPage;
    public GameObject spawnManager;
    

    private void Start()
    {
        spawnMatch = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }
    public void PlayTheGame()
    {
        //spawnMatch.readyToPlay = true;
        spawnManager.SetActive(true);
        initialPage.SetActive(false);
        //player.SetActive(true);
        UiManager.SetActive(true);
        playerUI.SetActive(true);
        //combatMap1.SetActive(true);
        //boardCamera.SetActive(false);
        //mainCamera.SetActive(true);

    }
}
