using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{    
    
    public static GameManager instance = null;

    public GameObject playerPrefab;
    
    public Vector3 spawnIncrements;
    public Vector3 spawnOffsets;
    public Vector3 spawnMinLimits;
    public Vector3 spawnMaxLimits;

    //private 
    

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
    }

    public void SpawnPlayer(GameObject playerObject) {
        float spawnX = 0 + spawnOffsets.x;
        float spawnY = 0 + spawnOffsets.y;
        float spawnZ = 0 + spawnOffsets.z;

        Vector3 randomPosition = new Vector3(spawnX, spawnY, spawnZ);
        Instantiate(playerObject, randomPosition, Quaternion.identity);
    }

    public void SpawnPlayer(GameObject playerObject, Vector3 position) {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer(playerPrefab);
    }

    

    // Update is called once per frame
    void Update()
    {
        // y
    }
}
