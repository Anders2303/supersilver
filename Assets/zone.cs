using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class zone : MonoBehaviour
{
    private float Timer;
    //private bool showTimer = false;
    //private bool showUser = false;
    private float TimeToComplete;
    //private bool playerInBounds;
    //private int playerCounter;
    
    public bool needsTwoPlayers;

    private bool localPlayerInBounds = false;
    private bool teammateInBounds = false;
    

    void Start()
    {
        Timer = Random.Range(15f, 20f); 
        TimeToComplete = Timer; 
        //playerInBounds = false; 
        //playerCounter = 0;  
    }


    void OnTriggerEnter(Collider other){
        /*playerInBounds = true;
        playerCounter += 1;

        Debug.Log(playerCounter);
        */
        if(other.gameObject.tag != "Player") {
            return;
        }
        Debug.Log(other);
        
        if (other.GetComponent<FirstPersonController>().hasAuthority) {
            localPlayerInBounds = true;
        } else {
            teammateInBounds = true;
        }
       

        Debug.Log(localPlayerInBounds);
        Debug.Log(teammateInBounds);
    }

    /*void OnTriggerStay(Collider other){
        if(other.gameObject.tag != "Player") {
            return;
        }

        Debug.Log(other);

        if (localPlayerInBounds && ( !needsTwoPlayers || teammateInBounds )){
            Timer -= Time.deltaTime;
            
            GameManager.instance.PrintToPlayer(((int) Timer) + " seconds remaining!");
            
            if (Timer < 0){
                Timer = 0;
                Destroy(gameObject);
                GameManager.instance.local_objectiveCompleted = true;
                //Debug.Log("Run for you life");
            }
        } else if(localPlayerInBounds && needsTwoPlayers) {
            GameManager.instance.PrintToPlayer("Objective needs two players!");
        }
    }*/

    void Update() {
        if (localPlayerInBounds && ( !needsTwoPlayers || teammateInBounds )){
            Timer -= Time.deltaTime;
            
            GameManager.instance.PrintToPlayer(((int) Timer) + " seconds remaining!");
            
            if (Timer < 0){
                Timer = 0;
                Destroy(gameObject);
                GameManager.instance.local_objectiveCompleted = true;
                //Debug.Log("Run for you life");
            }
        } else if(localPlayerInBounds && needsTwoPlayers) {
            GameManager.instance.PrintToPlayer("Objective needs two players!");
        }
    }


    void OnTriggerExit(Collider other){
        if(other.gameObject.tag != "Player") {
            return;
        }

        Debug.Log(other);
        
        Debug.Log("Exited the zone");
        if (Timer > 0){
            if (other.GetComponent<FirstPersonController>().hasAuthority) {
                localPlayerInBounds = false;
            } else {
                teammateInBounds = false;
            }
       
            GameManager.instance.PrintToPlayer("Left the objective!");

            //Debug.Log("You left to soon :( Start again");            
        }
        /*else {
            //Debug.Log("YES ! RUN ! ");
            Timer = Random.Range(5f, 15f);

        }
        showTimer = false;
        */
    }
   /*public void OnGUI() {
         if (showTimer) {
            GUI.Label(new Rect(10, 10, 100, 20), Timer.ToString());
         }
         if (showUser){
            GUI.Label(new Rect(10, 10, 100, 20), "Missing " + playerCounter.ToString() + " player(s)" );
         }
    }*/
}
