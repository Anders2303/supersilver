using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveBuildingChooser : MonoBehaviour
{
    public ObjectivePointChooser[] pointChooser;
    public void EnableRandomPoint() {
        pointChooser[Random.Range(0, pointChooser.Length)].EnableRandomObjective();
    }
}
