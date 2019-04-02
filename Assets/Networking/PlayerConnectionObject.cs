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

    [SyncVar]
    public bool isBadGuy;
    
    [SyncVar]
    public GameObject myController;

    
    // Start is called before the first frame update
    void Start()
    {
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
        } else {
            GameManager.instance.SetGameStatusText("Waiting for host to start game");
            Cmd_SpawnPlayerController();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isServer && !GameManager.instance.gameTimerOn && Input.GetKeyDown(KeyCode.O)) {
            Cmd_StartGame();
        } 
    }
    
    //public override void OnStartClient()
    //{
    //    Debug.Log("")
    //    GetComponent<Renderer>().material.color = myColor;
    //}

    //- SERVER COMMANDS -----------------------------------
    [Command]
    void Cmd_SpawnPlayerController() {
        GameObject player = Instantiate(playerControllerPrefab, GameManager.instance.GetRandomPlayerPosition(), Quaternion.identity);

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
        player.transform.LookAt(Vector3.zero);
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
