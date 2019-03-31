using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerConnectionObject : NetworkBehaviour
{
    /**
        Invisible player manager, will spawn relevant player prefab
    */
    
    public GameObject playerControllerPrefab;

    
    // Start is called before the first frame update
    void Start()
    {
        if(!isLocalPlayer){
            return;
        }

        Debug.Log("Creating personal player controller");
        // Spawn player controller
        Cmd_SpawnPlayerController();
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    //- SERVER COMMANDS -----------------------------------
    [Command]
    void Cmd_SpawnPlayerController() {
        GameObject player = Instantiate(playerControllerPrefab, GameManager.instance.GetRandomPlayerPosition(), Quaternion.identity);
        //player.GetComponent<FirstPersonController>().ForceLocalComponents();
        NetworkServer.SpawnWithClientAuthority(player, connectionToClient);
    }
}
