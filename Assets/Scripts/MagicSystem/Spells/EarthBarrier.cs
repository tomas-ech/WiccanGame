using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBarrier : MonoBehaviour
{
    public SpellScriptableObject spellInfo;

    void Awake()
    {
        Destroy(this.gameObject, spellInfo.Lifetime);
    }

    void Start()
    {
        
        
    }

    void Update()
    {
        
    }
}
