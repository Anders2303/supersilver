using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zone : MonoBehaviour
{
    private float Timer;
    private bool showLabel = false;
    private float TimeToComplete;
    void Start()
    {
    Timer = Random.Range(5f, 15f); 
    TimeToComplete = Timer;    
    }

    void OnTriggerStay(){
        showLabel = true;
        Timer -= Time.deltaTime;
        if (Timer < 0){
            Timer = 0;
            Debug.Log("Run for you life");
        }
    }

    void OnTriggerExit(){
        Debug.Log("Exited the zone");
        if (Timer > 0){
            
            Debug.Log("You left to soon :( Start again");
            
        }
        else {
            Debug.Log("YES ! RUN ! ");
            Timer = Random.Range(5f, 15f);

        }
        showLabel = false;

    }
    public void OnGUI() {
         if (showLabel) {
             GUI.Label(new Rect(10, 10, 100, 20), Timer.ToString());
         }
    }
}
