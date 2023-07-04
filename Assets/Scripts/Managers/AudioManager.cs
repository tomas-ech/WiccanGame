using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance {get; private set;}

    [SerializeField] private AudioSource[] sfx;
    public AudioSource[] bgm;
    private int bgmIndex;

    private void Awake( )
	{
		if(Instance != null && Instance != this)
		{
			Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);	
        }
    }   

    void Update()
    {
        /*if (!bgm[bgmIndex].isPlaying)
        {
            PlayRandomBGM();
        }*/
    }

    public void PlayRandomBGM()
    {
        bgmIndex = Random.Range(0, bgm.Length);

        PlayBGM(bgmIndex);
    }

    public void PlayBGM(int index)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }

        bgm[index].Play();
    }

    public void StopBGM()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }

    public void PlaySFX(int index)
    {
        sfx[index].Play();
    }

    public void PlayPitchedSFX(int index)
    {
        if (index< sfx.Length)
        {
            sfx[index].pitch = Random.Range(0.85f, 1.15f);
            sfx[index].Play();
        }
    }


}
