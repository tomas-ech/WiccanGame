using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSpell : MonoBehaviour
{
    //public SpellScriptableObject fireballSO;
    public SpellScriptableObject spellInfo;

    private SphereCollider myCollider;
    private Rigidbody myRB;


    void Awake()
    {
        myCollider = GetComponent<SphereCollider>();
        myCollider.isTrigger = true;
        myCollider.radius = spellInfo.SpellRadius;

        myRB = GetComponent<Rigidbody>();
        myRB.useGravity = true;
        myRB.isKinematic = false;

        Destroy(this.gameObject, spellInfo.Lifetime);
        
    }

    private void Start()
    {
        myRB.AddForce(Vector3.up * spellInfo.Speed * 4000 * Time.deltaTime, ForceMode.Impulse);
    }
    private void FixedUpdate()
    {
     
        myRB.AddForce(Physics.gravity * myRB.mass * 10);

        if (spellInfo.Speed > 0)
        {
            transform.Translate(Vector3.forward * spellInfo.Speed * Time.deltaTime);
        }
    }

    void Update()
    {
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Apply spell effects to whatever we hit
        //Apply hit particle effects
        //Apply sound effects

        if (other.CompareTag("Enemy") )//|| other.CompareTag("Player"))
        {
            PlayerStats healthComponent = other.GetComponent<PlayerStats>();
            healthComponent.characterCurrentHealth -= spellInfo.DamageAmount;   
            Debug.Log("Golpeado con Fuego!");
        }

        Destroy(this.gameObject);

    }
}
