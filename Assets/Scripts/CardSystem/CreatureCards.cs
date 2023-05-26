using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreatureCards : MonoBehaviour
{
    public PlayerScriptableObject characterInfo;

    [Header ("Card Design")]
    public Image splashArt;
    public Sprite splashArtSprite;
    public Image clanEdge;
    public Sprite clanEdgeSprite;
    public TextMeshProUGUI nameText;
    public Button cardSlotButton;
    public Color cardSlotColor;
    [Header ("Magic")]
    public GameObject earthIcon;
    public GameObject fireIcon;
    public GameObject spiritIcon;
    public GameObject waterIcon;
    public GameObject windIcon;
    
    

    void Update()
    {
        nameText.text = characterInfo.Name.ToString();
        splashArt.sprite = splashArtSprite;
        clanEdge.sprite = clanEdgeSprite;

        ColorBlock colorVar = cardSlotButton.colors;
        colorVar.highlightedColor = cardSlotColor;
        cardSlotButton.colors = colorVar;

        if(characterInfo.EarthMagic == true){earthIcon.SetActive(true);}
        if(characterInfo.FireMagic == true){fireIcon.SetActive(true);}
        if(characterInfo.spiritMagic == true){spiritIcon.SetActive(true);}
        if(characterInfo.WaterMagic == true){waterIcon.SetActive(true);}
        if(characterInfo.WindMagic == true){windIcon.SetActive(true);}
    }
}
