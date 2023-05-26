using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMissionTrigger : MonoBehaviour
{
    private UIManager uiManager;

    void Start()
    {
        uiManager = GameObject.Find("UiManager").GetComponent<UIManager>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.StopAllCoroutines();
            uiManager.StartCoroutine("Mission1");
            AudioManager.Instance.canvasMusicSeasonMap.Stop();
            AudioManager.Instance.canvasMusicSeasonMap.PlayOneShot(AudioManager.Instance.warMusic);
            AudioManager.Instance.canvasMusicSeasonMap.loop = true;
            AudioManager.Instance.canvasMusicSeasonMap.volume = 0.3f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}

