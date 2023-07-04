using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_Main : MonoBehaviour
{
    private bool gamePaused;

    public void SwitchMenu(GameObject uiMenu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        uiMenu.SetActive(true);

        AudioManager.Instance.PlaySFX(0); //Button Sound 0
    }

    public void PauseGame()
    {
        if (gamePaused)
        {
            Time.timeScale = 1;
            gamePaused = false;
        }
        else
        {
            Time.timeScale = 0;
            gamePaused = true;
        }
    }
}
