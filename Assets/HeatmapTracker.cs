using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class HeatmapTracker : MonoBehaviour
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

                if(lastPos == newPos) {
                    Destroy(lastObject);
                }

                lastObject = Instantiate(heatmapTrail, newPos, Quaternion.identity);
                trackingTimer = 0;
                lastPos = newPos;
            }
        } else {
            trackingTimer = positionTrackingFrequency;
        }
    }
}
