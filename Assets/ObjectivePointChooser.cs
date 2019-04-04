using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivePointChooser : MonoBehaviour
{
    public GameObject[] zones;
    public GameObject zoneTrigger;

    public void EnableRandomObjective()
    {
        int randomZoneIdx = Random.Range(0, zones.Length);
        Instantiate(zoneTrigger, zones[randomZoneIdx].transform.position, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        //EnableRandomObjective();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
