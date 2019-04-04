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
    public GameObject objectiveCounterText;
    public GameObject playerPrintText;
    public GameObject canvas;

    
    public float gameTime;

    public float ObjectiveDistanceFromPlayer;
    public bool local_objectiveCompleted = false;

    public int totalNumberOfObjectives = -1;
    public int objectivesCompleted = 0;

    private float playerNotificationLiveTime = 5;
    private float currentPlayerNotificationLiveTime = 5;

    public GameObject[] citySectors;

    public void PrintToPlayer(string message) {
        playerPrintText.GetComponent<UnityEngine.UI.Text>().text = message;
        currentPlayerNotificationLiveTime = playerNotificationLiveTime;
    }

    public void FlipCanvasVisible() {
        canvas.SetActive(!canvas.activeSelf);
    }


    public Vector3 GetPlayerSectorSpawn(int sector)
    {
        float spawnX = RandomSign()*spawnOffsets.x + citySectors[sector].transform.position.z;
        float spawnY = 1;
        float spawnZ = RandomSign()*spawnOffsets.z + citySectors[sector].transform.position.z;

        return new Vector3(spawnX, spawnY, spawnZ);
    }

    public void EnableObjectiveInSector(int[] sectors)
    {
        int sector = sectors[Random.Range(0, sectors.Length)];
        citySectors[sector].GetComponent<ObjectiveBuildingChooser>().EnableRandomPoint();
    }
    
    // This thing is an affront to god, avert your eyes
    public void EnableObjectiveRelativeToSector(int sector)
    {
        switch (sector)
        {
            case 0:
                EnableObjectiveInSector(new[] { 5, 7, 8 });
                break;
            case 1:
                EnableObjectiveInSector(new[] { 6, 7, 8 });
                break;
            case 2:
                EnableObjectiveInSector(new[] { 3, 6, 7});
                break;
            case 3:
                EnableObjectiveInSector(new[] { 2, 5, 8});
                break;
            case 4:
                EnableObjectiveInSector(new[] { 0, 2, 6, 8});
                break;
            case 5:
                EnableObjectiveInSector(new[] { 0, 3, 6});
                break;
            case 6:
                EnableObjectiveInSector(new[] { 1, 2, 5});
                break;
            case 7:
                EnableObjectiveInSector(new[] { 0, 1, 2});
                break;
            case 8:
                EnableObjectiveInSector(new[] { 0, 1, 3});
                break;
        }
    }

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

        playerPrintText.GetComponent<UnityEngine.UI.Text>().text = "";
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
        objectiveCounterText.GetComponent<UnityEngine.UI.Text>().text = objectivesCompleted + "/" + totalNumberOfObjectives + " objectives complete";

        if (currentPlayerNotificationLiveTime > 0)
        {
            currentPlayerNotificationLiveTime -= Time.deltaTime;
            if(currentPlayerNotificationLiveTime <= 0) {
                playerPrintText.GetComponent<UnityEngine.UI.Text>().text = "";
            } 
        }
        
        
    }
}
