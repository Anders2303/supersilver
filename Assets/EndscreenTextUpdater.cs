using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndscreenTextUpdater : MonoBehaviour
{
    public GameObject winningTeamText;
    // Start is called before the first frame update
    void Start()
    {
        winningTeamText.GetComponent<UnityEngine.UI.Text>().text = DataManager.instance.winningTeam + " wins!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
