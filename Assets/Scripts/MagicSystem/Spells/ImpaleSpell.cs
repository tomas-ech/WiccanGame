using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpaleSpell : MonoBehaviour
{
    public GameObject impaleHitFX;
    public SpellScriptableObject spellInfo;
    public int maximumLenght;
    public float separation, spawnDelay, damageDelay, height, radius, force, yOffSet;
    public LayerMask layerMask;

    [HideInInspector] public GameObject fxParent;
    [HideInInspector] public int currentLenght;
    private bool isLast = false, hasSpawnedNext = false, hasDamaged;
    private ParticleSystem particles;
    private float spawnDelayTimer, damageDelayTimer;    

    void Start()
    {
        //Revisa si es el primer objeto que se instancia
        if (currentLenght == 0){fxParent = gameObject;}

        particles = GetComponent<ParticleSystem>();

        spawnDelayTimer = spawnDelay;
        damageDelayTimer = damageDelay;
    }

    void Update()
    {
        if (spawnDelayTimer <= 0 && !hasSpawnedNext) {CreateImpaler();}

        if (spawnDelayTimer > 0) {spawnDelayTimer -= Time.deltaTime;}

        if (damageDelayTimer > 0) {damageDelayTimer -= Time.deltaTime;}

        if (damageDelayTimer <= 0 && !hasDamaged) {AOEDamage();}

        if (!particles.isPlaying && isLast) {Destroy(fxParent);}   
    }

    void CreateImpaler()
    {
        if (currentLenght < maximumLenght)
        {
            var rayCastPosition = transform.position + transform.forward * separation;
            rayCastPosition.y += height;
            RaycastHit hit;

            if (Physics.Raycast(rayCastPosition, Vector3.down, out hit, height +5, layerMask))
            {
                if (hit.transform != transform)
                {
                    var spawnLoc = hit.point;
                    spawnLoc.y += yOffSet;
                    hasSpawnedNext = true;
                    var obj = Instantiate(gameObject, transform);
                    obj.transform.position = spawnLoc;
                    obj.transform.rotation = transform.rotation;
                    var impale = obj.GetComponent<ImpaleSpell>();
                    impale.currentLenght = currentLenght + 1;
                    impale.maximumLenght = maximumLenght;
                    impale.fxParent = fxParent;
                }
                else {isLast = true;}

            }
            else {isLast = true;}
        }
        else {isLast = true;}
    }

    void AOEDamage()
    {
        //Set damage bool to avoid damaging unit endlessly
        hasDamaged = true;

        //Create a sphere around location using our radius
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, radius);

        //We get all colliders that overlap our sphere cast
        foreach (Collider col in objectsInRange)
        {
            //We get the enemies within range that contain a rigidbody
            Rigidbody enemy = col.GetComponent<Rigidbody>();

            //We check if enemy has been found
            if (enemy != null)
            {
                //We add a force upwards to the rigidbody
                StartCoroutine(EnemyStun());
                enemy.AddForce(Vector3.up* force, ForceMode.VelocityChange);
                
                //We create our impale fx hit
                var fx = Instantiate(impaleHitFX, enemy.transform.position, Quaternion.identity);

                //We destroy the fx on a delay depending on the duration of our fx
                Destroy(fx, 1);

                //You can also call your damaging script here
                PlayerStats healthComponent = col.GetComponent<PlayerStats>();
                healthComponent.characterCurrentHealth -= spellInfo.DamageAmount;   
            }

            IEnumerator EnemyStun()
            {
                enemy.isKinematic = true;
                yield return new WaitForSeconds(2);
                enemy.isKinematic = false;
            }
        }
    }
    
}
