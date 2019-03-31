using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideLights : MonoBehaviour
{
    private Light[] lights; 

    public enum myEnum // your custom enumeration
    {
        firstPersonLights, 
        mapLights
    };
    public myEnum hiddenLights = myEnum.firstPersonLights;  // this public var should appear as a drop down

    void Start()
    {
        Debug.Log("Setting lights");
        if(hiddenLights == myEnum.firstPersonLights){
            lights = GameManager.instance.firstPersonLights;
        }
        else if(hiddenLights == myEnum.mapLights){
            lights = GameManager.instance.mapLights;
        } else {
            Debug.Log("nothing!");
        }
    }
    
    void OnPreCull(){
        foreach (Light light in lights){
            light.enabled = false;
        }
    }
  
    void OnPostRender(){
        foreach (Light light in lights){
            light.enabled = true;
        }
    }
}
