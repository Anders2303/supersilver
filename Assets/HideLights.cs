using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideLights : MonoBehaviour
{
    public List<Light> Lights;
  
    void OnPreCull(){
        foreach (Light light in Lights){
            light.enabled = false;
        }
    }
  
    void OnPostRender(){
        foreach (Light light in Lights){
            light.enabled = true;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
