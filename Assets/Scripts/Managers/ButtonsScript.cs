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
        AudioManager.Instance.audioManager.PlayOneShot(AudioManager.Instance.buttonSound);
        spawnManager.randomPlay = true;
        initialPage.SetActive(false);
        UiManager.SetActive(true);
        playerUI.SetActive(true);
        boardCamera.SetActive(false);
    }

    public void TutorialCharacter()
    {
        AudioManager.Instance.audioManager.PlayOneShot(AudioManager.Instance.buttonSound);
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
        AudioManager.Instance.audioManager.PlayOneShot(AudioManager.Instance.buttonSound);
        AudioManager.Instance.canvasMusic.Stop();
        AudioManager.Instance.canvasMusicSeasonMap.Play();
        AudioManager.Instance.canvasMusicSeasonMap.volume = 0.3f;
    }

    public void CharacterSelected2()
    {
        tutorialSelectCharacter.SetActive(false);
        UiManager.SetActive(true);
        playerUI.SetActive(true);
        boardCamera.SetActive(false);
        spawnManager.tutorial2 = true;
        AudioManager.Instance.audioManager.PlayOneShot(AudioManager.Instance.buttonSound);
        AudioManager.Instance.canvasMusic.Stop();
        AudioManager.Instance.canvasMusicSeasonMap.Play();
        AudioManager.Instance.canvasMusicSeasonMap.volume = 0.3f;

    }


}
