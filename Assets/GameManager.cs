using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{    
    
    public static GameManager instance = null;
    
    public Vector3 spawnIncrements;
    public Vector3 spawnOffsets;
    public Vector3 spawnMinLimits;
    public Vector3 spawnMaxLimits;

    public Camera fullMapCamera;
    public GameObject spawnpoint;

    public Light[] mapLights;
    public Light[] firstPersonLights;


    public Camera getFullmapCamera(){
        return fullMapCamera;
    }

    void Awake()
    {
        //Check if instance already exists
        if (instance == null) {
            //if not, set instance to this
            instance = this;
        }
        //If instance already exists and it's not this:
        else if (instance != this) {            
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);    
        }
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        //MovePlayerToRandomPosition();
    }
    public Vector3 GetRandomPlayerPosition() {
        int increments  = Random.Range(-3, 3); 
        float spawnX = spawnIncrements.x*increments + spawnOffsets.x;
        increments  = Random.Range(-3, 3); 
        float spawnY = spawnIncrements.y*increments + spawnOffsets.y;
        increments  = Random.Range(-3, 3); 
        float spawnZ = spawnIncrements.z*increments + spawnOffsets.z;

        return new Vector3(spawnX, spawnY, spawnZ);
    }

    public void MovePlayerToRandomPosition(GameObject playerObject) {
        Debug.Log("moving");
        
        int increments  = Random.Range(-3, 3); 
        float spawnX = spawnIncrements.x*increments + spawnOffsets.x;
        increments  = Random.Range(-3, 3); 
        float spawnY = spawnIncrements.y*increments + spawnOffsets.y;
        increments  = Random.Range(-3, 3); 
        float spawnZ = spawnIncrements.z*increments + spawnOffsets.z;

        Vector3 randomPosition = new Vector3(spawnX, spawnY, spawnZ);
        playerObject.transform.position = randomPosition;
        //Instantiate(spawnpoint, randomPosition, Quaternion.identity);
    }

    public void GiveRandomColor(GameObject playerObject) {
        foreach (Renderer rend in playerObject.GetComponentsInChildren<Renderer>())
        {
            rend.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //SpawnPlayer(playerPrefab);
    }

    

    // Update is called once per frame
    void Update()
    {
        // y
    }
}
