using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zone : MonoBehaviour
{
    private float Timer;
    private bool showTimer = false;
    private bool showUser = false;
    private float TimeToComplete;
    private bool playerInBounds;
    private int playerCounter;
    void Start()
    {
    Timer = Random.Range(5f, 15f); 
    TimeToComplete = Timer; 
    playerInBounds = false; 
    playerCounter = 0;  
    }


    void OnTriggerEnter(){
        playerInBounds = true;
        playerCounter += 1;
    }

    void OnTriggerStay(){
        if (playerCounter >= 2){
            showTimer = true;
            Timer -= Time.deltaTime;
            if (Timer < 0){
            Timer = 0;
            Debug.Log("Run for you life");
            }
        }
    }


    void OnTriggerExit(){
        Debug.Log("Exited the zone");
        if (Timer > 0){
            playerCounter -= 1;
            
            Debug.Log("You left to soon :( Start again");
            
            
        }
        else {
            Debug.Log("YES ! RUN ! ");
            Timer = Random.Range(5f, 15f);

        }
        showTimer = false;

    }
    public void OnGUI() {
         if (showTimer) {
            GUI.Label(new Rect(10, 10, 100, 20), Timer.ToString());
         }
         if (showUser){
            GUI.Label(new Rect(10, 10, 100, 20), "Missing " + playerCounter.ToString() + " player(s)" );
         }
    }
}
