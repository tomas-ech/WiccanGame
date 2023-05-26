using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBarrier : MonoBehaviour
{
    public SpellScriptableObject spellInfo;
    private Transform playerPosition;

    private void Awake()
    {
        Destroy(this.gameObject, spellInfo.Lifetime);
    }

    private void Start()
    {
        playerPosition = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        transform.position = playerPosition.position;
    }

}
