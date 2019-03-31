using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkManager : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        //GameManager.instance.MovePlayerToRandomPosition(player);
        GameManager.instance.GiveRandomColor(player);
        
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}
