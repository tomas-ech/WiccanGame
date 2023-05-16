using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]


public class Spell : MonoBehaviour
{
    public SpellScriptableObject spellInfo;
    
    private SphereCollider myCollider;
    private Rigidbody myRB;
    
    void Awake()
    {
        myCollider = GetComponent<SphereCollider>();
        myCollider.isTrigger = true;
        myCollider.radius = spellInfo.SpellRadius;

        myRB = GetComponent<Rigidbody>();
        myRB.isKinematic = true;

        Destroy(this.gameObject, spellInfo.Lifetime);
        
    }

    void Update()
    {
        if (spellInfo.Speed > 0)
        {
            transform.Translate(Vector3.forward * spellInfo.Speed * Time.deltaTime);
        }
        
    }
}
