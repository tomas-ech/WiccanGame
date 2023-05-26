using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")]

public class SpellScriptableObject : ScriptableObject
{
    public string Name;
    public string Description;
    
    public bool EarthMagic;
    public bool FireMagic;
    public bool SpiritMagic;
    public bool WaterMagic;
    public bool WindMagic;

    public float DamageAmount = 10f;
    public float ManaCost = 5f;
    public float Lifetime = 2f;
    public float Speed = 15f;
    public float SpellRadius = 0.15f;
    public float SpellRange = 20f;
    
    //Status Effects
    //Thumbnail / miniatura
    //Time between casts
}
