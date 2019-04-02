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

    // public void UpdateColor(){
    //     Debug.Log("updating playerColor");
    // }
    
    // public override void OnStartClient()
    // {
    //     UpdateColor();
    // }

    
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

            Cmd_SpawnPlayerController_badguy();
        } else {
            Cmd_SpawnPlayerController();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    void Cmd_SpawnPlayerController_badguy() {
        GameObject player = Instantiate(playerControllerPrefab_badguy, GameManager.instance.GetRandomPlayerPosition_badguy(), Quaternion.identity);

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

    //Client RCP
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
