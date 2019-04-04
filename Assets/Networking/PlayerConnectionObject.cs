using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerConnectionObject : NetworkBehaviour
{
    /**
        Invisible player manager, will spawn relevant player prefab
    **/
    [SyncVar]
    public Color playerColor;

    public Color playerColor_badguy;


    public GameObject playerControllerPrefab;
    public GameObject playerControllerPrefab_badguy;

    private int ObjectivesCompleted = 0;

    [SyncVar]
    public bool isBadGuy;
    
    [SyncVar]
    public GameObject myController;

    private bool gameHasEnded = false;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.totalNumberOfObjectives ++;
        if(!isLocalPlayer){
            // Kinda hacky. Sets the playerColors on all previously connect clients, (and possibly new ones?)
            if (myController != null)
            {
                foreach (Renderer rend in myController.GetComponentsInChildren<Renderer>())
                {
                    rend.material.color = playerColor;  
                }
            }
            return;
        }

        //Debug.Log("Creating personal player controller");
        // Spawn player controller
        if(isServer) {
            playerColor = playerColor_badguy;
            isBadGuy = true;
            GameManager.instance.SetGameStatusText("Press the \"O\" key to start game");
            Cmd_SpawnPlayerController_badguy();
            //Cmd_SpawnPlayerController();
        } else {
            GameManager.instance.SetGameStatusText("Waiting for host to start game");
            int startingSector = Random.Range(0, 10);
            Cmd_SpawnPlayerController(startingSector);
            GameManager.instance.EnableObjectiveRelativeToSector(startingSector);
        }
        
    }

    void Disconnect() {
        if(isServer){
            NetworkManager.singleton.StopHost();
        }
        NetworkManager.singleton.StopClient();
    }
    // Update is called once per frame
    void Update()
    {
        
        if(!isLocalPlayer) {
            return;
        }
        if (isServer && !GameManager.instance.gameTimerOn && Input.GetKeyDown(KeyCode.O)) {
            Cmd_StartGame();
        }

        if(GameManager.instance.local_objectiveCompleted) {
            Cmd_registerObjectiveComplete();
            GameManager.instance.local_objectiveCompleted = false;
        }

        if(!gameHasEnded && GameManager.instance.gameTime <= 0) {
            Debug.Log("Time ran out");
            gameHasEnded = true;
            if(isServer) {
                Cmd_EndGame("The Agent");
            }
        }
    }
    
    //public override void OnStartClient()
    //{
    //    Debug.Log("")
    //    GetComponent<Renderer>().material.color = myColor;
    //}

    //- SERVER COMMANDS -----------------------------------
    [Command]
    void Cmd_registerObjectiveComplete() {
        if(GameManager.instance.objectivesCompleted+1 >= GameManager.instance.totalNumberOfObjectives) {
            Cmd_EndGame("The Resistance");
        } else {
            Rpc_registerObjectiveComplete();
        }
    }

    [ClientRpc]
    void Rpc_registerObjectiveComplete() {
        GameManager.instance.objectivesCompleted ++;
    }

    [Command]
    void Cmd_EndGame(string winningTeam) 
    {
        Debug.Log("Changing scene");
        DataManager.instance.winningTeam = winningTeam;          
        Rpc_SetWinningTeam(winningTeam);      
        NetworkManager.singleton.ServerChangeScene("Endscreen");
    }

    [ClientRpc]
    void Rpc_SetWinningTeam(string winner)
    {
        DataManager.instance.winningTeam = winner;
    }

    [Command]
    void Cmd_SpawnPlayerController(int startingSector) {
        
        GameObject player = Instantiate(playerControllerPrefab, GameManager.instance.GetPlayerSectorSpawn(startingSector), Quaternion.identity);
        //GameManager.instance.GiveRandomColor(player);
        //player.GetComponent<FirstPersonController>().SetOwner(this);
        Debug.Log("Updating server");
        foreach (Renderer rend in player.GetComponentsInChildren<Renderer>())
        {
            rend.material.color = playerColor;
        }

        NetworkServer.SpawnWithClientAuthority(player, connectionToClient);
        
        Rpc_updatecolor(player);
        Rpc_setOwner(player);
    }

    [Command]
    void Cmd_StartGame()
    {
        GameManager.instance.StartGame();
        Rpc_StartGame();
    }

    [Command]
    void Cmd_SpawnPlayerController_badguy() {
        GameObject player = Instantiate(playerControllerPrefab_badguy, GameManager.instance.GetRandomPlayerPosition_badguy(), Quaternion.identity);
        //player.transform.LookAt(Vector3.zero);
        transform.rotation = Quaternion.Euler(45,0,0);
        //GameManager.instance.GiveRandomColor(player);
        //player.GetComponent<FirstPersonController>().SetOwner(this);
        Debug.Log("Updating server");
        foreach (Renderer rend in player.GetComponentsInChildren<Renderer>())
        {
            rend.material.color = playerColor;
        }

        NetworkServer.SpawnWithClientAuthority(player, connectionToClient);
        
        Rpc_updatecolor(player);
        Rpc_setOwner(player);
    }

    // [ClientRpc]
    // void Rpc_setWinningTeam(string team)
    // {
    //     DataManager.instance
    // }
    //Client RPC
    [ClientRpc]
    void Rpc_StartGame()
    {
        GameManager.instance.StartGame();
    }

    [ClientRpc]
    void Rpc_setOwner(GameObject playerObject) {
        myController = playerObject;
        //playerObject.GetComponent<FirstPersonController>().SetOwner(this);
    }

    [ClientRpc]
    void Rpc_updatecolor(GameObject playerObject) {
        Debug.Log("Updating client");
        foreach (Renderer rend in playerObject.GetComponentsInChildren<Renderer>())
        {
            rend.material.color = playerColor;
        }

    }
}
