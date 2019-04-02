using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{    
    
    public static GameManager instance = null;
    public bool dontDestroyOnLoad;

    public Vector3 spawnIncrements;
    public Vector3 spawnOffsets;
    public Vector3 spawnMinLimits;
    public Vector3 spawnMaxLimits;

    public Camera fullMapCamera;
    public GameObject spawnpoint;

    public Light[] mapLights;
    public Light[] firstPersonLights;

    public bool gameTimerOn = false;

    public GameObject gameStatusText;
    
    public float gameTime;

    

    public Camera getFullmapCamera(){
        return fullMapCamera;
    }

    public int RandomSign() {
        return Random.value < .5? 1 : -1;
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
        if (dontDestroyOnLoad) {
            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);
            //MovePlayerToRandomPosition();
        }
    }
    public void SetGameStatusText(string newText) {
        gameStatusText.GetComponent<UnityEngine.UI.Text>().text = newText;
    }


    public Vector3 GetRandomPlayerPosition() 
    {
        int increments  = Random.Range(-4, 4); 
        float spawnX = spawnIncrements.x*increments + spawnOffsets.x;
        increments  = Random.Range(-4, 4); 
        float spawnY = spawnIncrements.y*increments + spawnOffsets.y;
        increments  = Random.Range(-4, 4); 
        float spawnZ = spawnIncrements.z*increments + spawnOffsets.z;

        return new Vector3(spawnX, spawnY, spawnZ);
    }

    public Vector3 GetRandomPlayerPosition_badguy() 
    {
        float spawnX = RandomSign()*spawnMaxLimits.x;  
        float spawnY = 1;
        float spawnZ = RandomSign()*spawnMaxLimits.x;

        return new Vector3(spawnX, spawnY, spawnZ);
    }

    // public void MovePlayerToRandomPosition(GameObject playerObject) {
    //     Debug.Log("moving");
        
    //     int increments  = Random.Range(-3, 3); 
    //     float spawnX = spawnIncrements.x*increments + spawnOffsets.x;
    //     increments  = Random.Range(-3, 3); 
    //     float spawnY = spawnIncrements.y*increments + spawnOffsets.y;
    //     increments  = Random.Range(-3, 3); 
    //     float spawnZ = spawnIncrements.z*increments + spawnOffsets.z;

    //     Vector3 randomPosition = new Vector3(spawnX, spawnY, spawnZ);
    //     playerObject.transform.position = randomPosition;
    //     //Instantiate(spawnpoint, randomPosition, Quaternion.identity);
    // }

    public Color GetRandomPlayerColor() {
        return Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        
    }

    public void StartGame() {
        gameTimerOn = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        //SpawnPlayer(playerPrefab);
    }

    

    // Update is called once per frame
    void Update()
    {
        if(gameTimerOn) {
            gameTime -= Time.deltaTime;
            
            int minutes = Mathf.FloorToInt(gameTime / 60F);
            int seconds = Mathf.FloorToInt(gameTime - minutes * 60);
            string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

            gameStatusText.GetComponent<UnityEngine.UI.Text>().text = "Time remaining: " + niceTime;
        }
    }
}
