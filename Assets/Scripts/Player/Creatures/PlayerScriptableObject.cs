using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Creature", menuName = "Creature")]

public class PlayerScriptableObject : ScriptableObject
{
    public string Name;
    public string Description;
    public string Clan;
    public bool spiritMagic;
    public bool EarthMagic;
    public bool WindMagic;
    public bool WaterMagic;
    public bool FireMagic;
    public float Speed = 200f;
    public float Health = 1000f;
    public float Mana = 1000f;
    public float Strength = 100;
    public float Agility = 100;
    public float Intellect = 100;
    public float Spirit = 100;

}
