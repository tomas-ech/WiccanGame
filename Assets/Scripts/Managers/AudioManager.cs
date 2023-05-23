using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioManager;
    public AudioSource canvasMusic;
    public AudioSource canvasMusicSeasonMap;
    public AudioSource canvasMusicLavaMap;
    public AudioSource walkingSound;

    [Header("UISoundFX")]
    public AudioClip buttonSound;
    public AudioClip seasonMapMusic;

    [Header("GameSoundFX")]
    public AudioClip throwFire;
    public AudioClip explosionSound;
    public AudioClip  rockAttack;
    public AudioClip windPunch;
    public AudioClip blizzard;
    public AudioClip frostShardImpact;
    //public AudioClip
    //public AudioClip

    public static AudioManager Instance {get; private set;}

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

    void Start()
    {

    }

}
