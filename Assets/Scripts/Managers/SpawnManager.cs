using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> charactersArray = new List<GameObject>();
    public GameObject[] locationsArray;
    public GameObject[] spawnPointsSeasonMap;
    public GameObject[] spawnPointsLavaMap;
    private DropZone dropZone;
    private int randomLocation;
    private int randomCharacter;
    public bool randomPlay = false;
    public bool tutorial1 = false;
    public bool tutorial2 = false;

    /*private void OnEnable()
    {
        randomLocation = Random.Range(0, locationsArray.Length);

        //Organizar mapas como map1, map2... 
        Instantiate(locationsArray[randomLocation], locationsArray[randomLocation].transform.position, locationsArray[randomLocation].transform.rotation);

        randomCharacter = Random.Range(0, charactersArray.Count);

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
    }*/

    void Start()
    {
        dropZone = GetComponent<DropZone>();
    }

    void Update()
    {
        if (randomPlay == true)
        {
            randomLocation = Random.Range(0, locationsArray.Length);

            //Organizar mapas como map1, map2... 
            Instantiate(locationsArray[randomLocation], locationsArray[randomLocation].transform.position, locationsArray[randomLocation].transform.rotation);

            randomCharacter = Random.Range(0, charactersArray.Count);

            if (randomLocation == 0)
            {
                int randomSpawn = Random.Range(0, spawnPointsSeasonMap.Length);

                Instantiate(charactersArray[randomCharacter], spawnPointsSeasonMap[randomSpawn].transform.position, charactersArray[randomCharacter].transform.localRotation);
                
            }

            if (randomLocation == 1)
            {
                int randomSpawn = Random.Range(0, spawnPointsLavaMap.Length);

                //Cambiar el personaje random por la varibale referenceNumber que tiene el script DropZone
                Instantiate(charactersArray[randomCharacter], spawnPointsLavaMap[randomSpawn].transform.position, charactersArray[randomCharacter].transform.rotation);
            }

            randomPlay = false;
        }

        if (tutorial1 == true)
        {
            Instantiate(locationsArray[0], locationsArray[0].transform.position, locationsArray[0].transform.rotation);

            Instantiate(charactersArray[0], spawnPointsSeasonMap[1].transform.position, charactersArray[0].transform.localRotation);

            tutorial1 = false;
        }

        if (tutorial2 == true)
        {
            Instantiate(locationsArray[0], locationsArray[0].transform.position, locationsArray[0].transform.rotation);

            Instantiate(charactersArray[1], spawnPointsSeasonMap[1].transform.position, charactersArray[1].transform.localRotation);

            tutorial2 = false;
        }

        
    }
}
