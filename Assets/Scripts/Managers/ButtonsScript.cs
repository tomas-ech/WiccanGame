    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsScript : MonoBehaviour
{
    private SpawnManager spawnManager;
    public GameObject UiManager;
    public GameObject playerUI;
    public GameObject tutorialSelectCharacter;
    public GameObject boardCamera;
    public GameObject tutorialTexts;
    public GameObject initialPage;
    public GameObject spawnObj;
    public GameObject menuPage;
    public GameObject lorePage;


    private GameObject playerCamera;
    

    private void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }


    public void ReturnToMainMenu()
    {
        AudioManager.Instance.PlaySFX(0);
        initialPage.SetActive(true);
        tutorialSelectCharacter.SetActive(false);
        lorePage.SetActive(false);
    }

    public void ExitGame()
    {
        AudioManager.Instance.PlaySFX(0);
        Application.Quit();
    }

    public void ReturnToMainScene()
    {
        AudioManager.Instance.PlayBGM(0);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void PlayTheGame()
    {
        AudioManager.Instance.PlaySFX(0);
        spawnManager.randomPlay = true;
        initialPage.SetActive(false);
        UiManager.SetActive(true);
        playerUI.SetActive(true);
        boardCamera.SetActive(false);
    }

    public void CharacterSelected1()
    {
        spawnManager.tutorial1 = true;

        tutorialSelectCharacter.SetActive(false);
        UiManager.SetActive(true);
        playerUI.SetActive(true);
        boardCamera.SetActive(false);
        tutorialTexts.SetActive(true);

        AudioManager.Instance.PlaySFX(0);
        AudioManager.Instance.PlayBGM(1);
    }

    public void CharacterSelected2()
    {
        spawnManager.tutorial2 = true;

        tutorialSelectCharacter.SetActive(false);
        UiManager.SetActive(true);
        playerUI.SetActive(true);
        boardCamera.SetActive(false);
        tutorialTexts.SetActive(true);

        AudioManager.Instance.PlaySFX(0);
        AudioManager.Instance.PlayBGM(1);
    }
}
