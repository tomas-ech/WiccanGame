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


    private GameObject playerCamera;
    

    private void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void ReturnToMainMenu()
    {
        AudioManager.Instance.audioManager.PlayOneShot(AudioManager.Instance.buttonSound);
        initialPage.SetActive(true);
        tutorialSelectCharacter.SetActive(false);
    }

    public void ExitGame()
    {
        AudioManager.Instance.audioManager.PlayOneShot(AudioManager.Instance.buttonSound);
        Application.Quit();
    }

    public void ReturnToMainScene()
    {
        AudioManager.Instance.audioManager.PlayOneShot(AudioManager.Instance.buttonSound);
        AudioManager.Instance.audioManager.Stop();
        SceneManager.LoadScene(0);
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
        spawnManager.tutorial1 = true;

        tutorialSelectCharacter.SetActive(false);
        UiManager.SetActive(true);
        playerUI.SetActive(true);
        boardCamera.SetActive(false);
        tutorialTexts.SetActive(true);

        AudioManager.Instance.audioManager.PlayOneShot(AudioManager.Instance.buttonSound);
        AudioManager.Instance.canvasMusic.Stop();
        AudioManager.Instance.canvasMusicSeasonMap.Play();
        AudioManager.Instance.canvasMusicSeasonMap.volume = 0.1f;
    }

    public void CharacterSelected2()
    {
        spawnManager.tutorial2 = true;

        tutorialSelectCharacter.SetActive(false);
        UiManager.SetActive(true);
        playerUI.SetActive(true);
        boardCamera.SetActive(false);
        tutorialTexts.SetActive(true);

        AudioManager.Instance.audioManager.PlayOneShot(AudioManager.Instance.buttonSound);
        AudioManager.Instance.canvasMusic.Stop();
        AudioManager.Instance.canvasMusicSeasonMap.Play();
        AudioManager.Instance.canvasMusicSeasonMap.volume = 0.1f;
    }
}
