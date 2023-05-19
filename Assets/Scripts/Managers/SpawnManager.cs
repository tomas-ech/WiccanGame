using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] charactersArray;
    //public GameObject characterToPlay;
    //public bool readyToPlay = false;
    public GameObject player1;
    public GameObject[] locationsArray;
    public GameObject[] spawnPointsSeasonMap;
    public GameObject[] spawnPointsLavaMap;
    private DropZone dropZone;
    private int randomLocation;
    private int randomCharacter;

    private void OnEnable()
    {
        randomLocation = Random.Range(0, locationsArray.Length);

        //Organizar mapas como map1, map2... 
        Instantiate(locationsArray[randomLocation], locationsArray[randomLocation].transform.position, locationsArray[randomLocation].transform.rotation);

        randomCharacter = Random.Range(0, charactersArray.Length);

        if (randomLocation == 0)
        {
            int randomSpawn = Random.Range(0, spawnPointsLavaMap.Length);

            //Cambiar el personaje random por la varibale referenceNumber que tiene el script DropZone
            Instantiate(charactersArray[randomCharacter], spawnPointsLavaMap[randomSpawn].transform.position, charactersArray[randomCharacter].transform.rotation);
        }

        if (randomLocation == 1)
        {
            int randomSpawn = Random.Range(0, spawnPointsSeasonMap.Length);

            Instantiate(charactersArray[randomCharacter], spawnPointsSeasonMap[randomSpawn].transform.position, charactersArray[randomCharacter].transform.localRotation);
        }
    }

    void Start()
    {
        dropZone = GetComponent<DropZone>();
    }

    /*void Update()
    {
        characterToPlay = dropZone.referenceObj;

        if (readyToPlay == true)
        {
            randomLocation = Random.Range(0, locationsArray.Length);

            //Organizar mapas como map1, map2... 
            Instantiate(locationsArray[randomLocation], locationsArray[randomLocation].transform.position, locationsArray[randomLocation].transform.rotation);

            //randomCharacter = Random.Range(0, charactersArray.Length);

            if (randomLocation == 0)
            {
                int randomSpawn = Random.Range(0, spawnPointsLavaMap.Length);

                //Cambiar el personaje random por la varibale referenceNumber que tiene el script DropZone
                Instantiate(characterToPlay, spawnPointsLavaMap[randomSpawn].transform.position, characterToPlay.transform.rotation);
            }

            if (randomLocation == 1)
            {
                int randomSpawn = Random.Range(0, spawnPointsSeasonMap.Length);

                Instantiate(characterToPlay, spawnPointsSeasonMap[randomSpawn].transform.position, characterToPlay.transform.localRotation);
            }
        }
    }*/
}
