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
            uiManager.remainingEnemies.SetActive(true);
            if(!AudioManager.Instance.bgm[5].isPlaying)
            {
                AudioManager.Instance.PlayBGM(5);
            }
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

