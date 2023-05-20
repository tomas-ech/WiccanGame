using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreatureCards : MonoBehaviour
{
    public PlayerScriptableObject characterInfo;
    public Image splashArt;
    public Sprite splashArtSprite;
    public Image clanEdge;
    public Sprite clanEdgeSprite;
    public TextMeshProUGUI nameText;
    public Button cardSlotButton;
    public Color cardSlotColor;
    

    void Update()
    {
        nameText.text = characterInfo.Name.ToString();
        splashArt.sprite = splashArtSprite;
        clanEdge.sprite = clanEdgeSprite;

        ColorBlock colorVar = cardSlotButton.colors;
        colorVar.highlightedColor = cardSlotColor;
        cardSlotButton.colors = colorVar;
    }
}
