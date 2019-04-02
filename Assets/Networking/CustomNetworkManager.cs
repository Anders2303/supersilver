using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkManager : NetworkManager
{
    
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        
        GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        
        player.GetComponent<PlayerConnectionObject>().playerColor = GameManager.instance.GetRandomPlayerColor();

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

        /*/if(conn.playerControllers.Count > 0){
            
            foreach (UnityEngine.Networking.PlayerController pc in conn.playerControllers)
            {
                PlayerConnectionObject pco = pc.gameObject.GetComponent<PlayerConnectionObject>();
                pco.UpdateColor();
                
            }
            //GameObject player = conn.playerControllers[0].gameObject;
            // do stuff to the player GameObject
            
        }*/
    }
    
}
