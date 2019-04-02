using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.Networking;

public class HeatmapTracker : NetworkBehaviour
{
    public float positionTrackingFrequency; // How often to store location
    public GameObject heatmapTrail;
    public float trailSpawnHeight;

    private float trackingTimer;
    private Vector3 lastPos;
    private GameObject lastObject;

    private FirstPersonController firstPersonController;
            
    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
        trackingTimer = positionTrackingFrequency;
        firstPersonController = GetComponent<FirstPersonController>();
        //Instantiate(heatmapTrail, lastPos, Quaternion.identity);
            
    }

    // Update is called once per frame
    void Update()
    {
        if (firstPersonController.IsMapUp())
        {
            trackingTimer += 1 * Time.deltaTime;

            if(trackingTimer > positionTrackingFrequency){
                Vector3 newPos = transform.position;
                newPos.y = 194.6f;

                //if(lastPos == newPos) {
                //    Destroy(lastObject);
                //}

                Cmd_SpawnHeatMapObject(newPos);
                
                trackingTimer = 0;
                lastPos = newPos;
            }
        } else {
            trackingTimer = positionTrackingFrequency;
        }
    }

    [Command]
    public void Cmd_SpawnHeatMapObject(Vector3 newPos) {
        GameObject go = Instantiate(heatmapTrail, newPos, Quaternion.identity);
            
        //player.GetComponent<FirstPersonController>().ForceLocalComponents();
        NetworkServer.Spawn(go);
    }
}
