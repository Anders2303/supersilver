using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance = null;
    
    public string winningTeam = "";
    
    void Awake()
    {
        //Check if instance already exists
        if (instance == null) {
            //if not, set instance to this
            instance = this;
        }
        //If instance already exists and it's not this:
        else if (instance != this) {            
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a DataManager.
            Destroy(gameObject);    
        }

        DontDestroyOnLoad(gameObject);
    }
}
