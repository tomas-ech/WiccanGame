using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyProjectile : MonoBehaviour
{
    public SpellScriptableObject spellInfo;

    private SphereCollider myCollider;
    private Rigidbody myRB;
    private GameObject impactFx;
    public string[] tagsToCheck;


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
        //myRB.AddForce(Vector3.up * spellInfo.Speed * 4000 * Time.deltaTime, ForceMode.Impulse);
        impactFx = transform.Find("ImpactFx").gameObject;
    }
    private void FixedUpdate()
    {
     
        //myRB.AddForce(Physics.gravity * myRB.mass * 10);

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

        if (other.CompareTag("Player"))
        {
            PlayerStats healthComponent = other.GetComponent<PlayerStats>();
            healthComponent.characterCurrentHealth -= spellInfo.DamageAmount; 
            
            StartCoroutine(GettingHit(other));

            Debug.Log("Golpeado con Fuego!");
        }

        if (tagsToCheck.Contains(other.tag))
        {
            Collider[] objectsInRange = Physics.OverlapSphere(transform.position, 1);

            foreach(Collider col in objectsInRange)
            {
                Rigidbody enemy = col.GetComponent<Rigidbody>();

                if (enemy != null)
                {
                    Destroy(gameObject);
                }
            }

            impactFx.SetActive(true);
            impactFx.transform.SetParent(null);
            AudioManager.Instance.audioManager.PlayOneShot(AudioManager.Instance.explosionSound);
            Destroy(impactFx, 0.5f);
            Destroy(gameObject);
        }

    }

    IEnumerator GettingHit(Collider other)
    {
        Animator animator = other.GetComponentInChildren<Animator>();
        animator.SetBool("Hit", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Hit", false);
    }
}
