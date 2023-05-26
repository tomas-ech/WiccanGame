using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MouseOverInfo : MonoBehaviour
{
    public TextMeshProUGUI CharacterName, description, strength, agility, intellect, spirit;
    public GameObject infoBackground;

    private void OnMouseEnter()
    {
        CreatureCards creatureInfo = GetComponent<CreatureCards>();
        infoBackground.SetActive(true);
        CharacterName.text = creatureInfo.characterInfo.Name;
        
    }
}
