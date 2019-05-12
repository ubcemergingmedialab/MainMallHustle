using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Sirenix.OdinInspector;
using UnityEngine;

public class Populator : MonoBehaviour
{
    // public int NumberOfTargets;
    /*
     * Because of how A* pathfinder works, when increasing the size of the target list, it will fill the new array slots with duplicates of the very last
     * place in the array. When modifying the size of the array, input the size, hit enter, and then click on Update Targets to populate the list with 
     * new targets. 
     * It's important to click update when deleting targets as well as this will get rid of targets created in the unity scene. 
     * For the time being if errors arise, delete all the targets manually and populate the target list from scratch. 
     * Set size to 0 and click update targets twice to reset everything. 
     * 
     * 
     * Current bug:
     * Pick size n for targets and populate. 
     * Pick a smaller size m and don't update targets
     * Pick a bigger size than the initial k
     * Populate
     * The targets between n and m wont populate
     * trying to input a smaller size after this happens will result in a MissingReferenceException
     * 
     * Technically this won't happen a lot aslong as we press UpdateTargets after every single action on the Patrol script list, but it leaves too big a room for error. 
     * 
     * 
     * another Bug:
     * This one is more substantial. The target lists are emptying when we press play. Why
     * 
     * Click and draggin the targets into the lists manually makes it work??
     * */

    [Button("Update targets")]
    void UpdateTargets()
    {
        {
            Patrol_AutoTarget pa = gameObject.GetComponent<Patrol_AutoTarget>();
            pa.UpdateTargets();
        }
    }
     /*   
    [Button("Delete target")]
    void DeleteTarget()
    {
        Patrol_AutoTarget pa = gameObject.GetComponent<Patrol_AutoTarget>();
        pa.DeleteTarget(ToDelete);
    }*/
    
}
