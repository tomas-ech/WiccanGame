using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsScript : MonoBehaviour
{
    [SerializeField] private GameObject[] characterArray;
    private SpawnManager spawnManager;
    public GameObject UiManager;
    public GameObject playerUI;
    public GameObject tutorialSelectCharacter;
    public GameObject boardCamera;
    //public GameObject mainCamera;
    public GameObject initialPage;
    public GameObject spawnObj;


    

    private void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }
    public void PlayTheGame()
    {
        spawnManager.randomPlay = true;
        initialPage.SetActive(false);
        UiManager.SetActive(true);
        playerUI.SetActive(true);
        boardCamera.SetActive(false);
    }

    public void TutorialCharacter()
    {
        initialPage.SetActive(false);
        tutorialSelectCharacter.SetActive(true);
    }

    public void CharacterSelected1()
    {
        tutorialSelectCharacter.SetActive(false);
        UiManager.SetActive(true);
        playerUI.SetActive(true);
        boardCamera.SetActive(false);
        spawnManager.tutorial1 = true;
    }

    public void CharacterSelected2()
    {
        tutorialSelectCharacter.SetActive(false);
        UiManager.SetActive(true);
        playerUI.SetActive(true);
        boardCamera.SetActive(false);
        spawnManager.tutorial2 = true;
    }


}
