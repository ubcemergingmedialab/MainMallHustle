using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace Pathfinding
{




    /*  This code is based of Patrol.cs found in the A* Pathfinder asset. 
     * 
     * targets is becoming empty on load, but retains the size it had before the project closed
     * 
     * 
     * */
    /// <summary>
    /// Simple patrol behavior.
    /// This will set the destination on the agent so that it moves through the sequence of objects in the <see cref="targets"/> array.
    /// Upon reaching a target it will wait for <see cref="delay"/> seconds.
    ///
    /// See: <see cref="Pathfinding.AIDestinationSetter"/>
    /// See: <see cref="Pathfinding.AIPath"/>
    /// See: <see cref="Pathfinding.RichAI"/>
    /// See: <see cref="Pathfinding.AILerp"/>
    /// </summary>
    [UniqueComponent(tag = "ai.destination")]
    [HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_patrol.php")]
    public class Patrol_AutoTarget : VersionedMonoBehaviour
    {
        /// <summary>Target points to move to in order</summary>



        public Transform[] targets;

        /// <summary>Time in seconds to wait at each target</summary>
        public float delay = 0;

        /// <summary>Current target index</summary>
        int index;

        IAstarAI agent;
        float switchTime = float.PositiveInfinity;

        public GameObject TargetHolder;



        protected override void Awake()
        {
            // Figure out method to call update targets on unity scene load and we're good
            UpdateTargets();
            base.Awake();
            agent = GetComponent<IAstarAI>();
        }

        /// <summary>Update is called once per frame</summary>
        void Update()
        {
            if (targets.Length == 0) return;

            bool search = false;

            // Note: using reachedEndOfPath and pathPending instead of reachedDestination here because
            // if the destination cannot be reached by the agent, we don't want it to get stuck, we just want it to get as close as possible and then move on.
            if (agent.reachedEndOfPath && !agent.pathPending && float.IsPositiveInfinity(switchTime))
            {
                switchTime = Time.time + delay;
            }

            if (Time.time >= switchTime)
            {
                index = index + 1;
                search = true;
                switchTime = float.PositiveInfinity;
            }

            index = index % targets.Length;
            agent.destination = targets[index].position;

            if (search) agent.SearchPath();
        }

        // ===============================================================================================
        // Added code by William Beltran 07/11/18


        private Transform[] prevTargets;


        //Default Patrol code

        int PrevSize = 0;

        public void UpdateTargets()
        {
            int TargetNumber = TargetHolder.transform.childCount;

            // TODO
            // If targets.length is equal to targetNumber then we want to iterate through targets length and make sure that targets is pointing
            // to the elements in targetHolder
            if (TargetNumber == targets.Length)
            {
                for (int i = 0; i < TargetNumber; i++)
                {
                    targets[i] = TargetHolder.transform.GetChild(i);
                }
            }

            // If targets.length is greater than TargetNumber then we want to generate more Targets
            else if (targets.Length > TargetNumber)
            {
                for (int i = 0; i < TargetNumber; i++)
                {
                    targets[i] = TargetHolder.transform.GetChild(i);
                }
                for (int i = TargetNumber; i < targets.Length; i++)
                {
                    GameObject g = new GameObject();
                    g.transform.parent = TargetHolder.transform;
                    g.name = "Target " + i;
                    targets[i] = g.transform;
                    targets[i].SetPositionAndRotation(gameObject.transform.position,new Quaternion(0,0,0,0));
                }
            }
            // If target length is less than target number then we want to remove items in targetholder 
            else
            {
              
                int PrevChildSize = TargetHolder.transform.childCount;
                List<GameObject> ToDelete = new List<GameObject>();

                    for (int i = targets.Length; i < TargetNumber; i++)
                    {
                       ToDelete.Add(TargetHolder.transform.GetChild(i).gameObject);
                    }

                    foreach(GameObject g in ToDelete)
                {
                   /* UnityEditor.EditorApplication.delayCall += () =>
                    {
                        DestroyImmediate(g);
                    };*/
                }
               
                    
            }

            /*
             if (TargetNumber == targets.Length)
             {

             }





             Transform[] ToDelete;
             if (targets.Length < PrevSize)
             {
                 ToDelete = new Transform[prevTargets.Length];
                 int j = 0;
                 for (int i = targets.Length; i < PrevSize; i++)
                 {
                     ToDelete[j] = prevTargets[i];
                     j++;
                 }
                 // This is how far ToDelete must traverse to delete the removed targets
                 int finalIndex = j;
                 UnityEditor.EditorApplication.delayCall += () =>
                 {
                     for (int i = 0; i < finalIndex; i++)
                     {
                         print("toDelete Length : " + ToDelete.Length);
                         print("Current Index: " + i);
                         DestroyImmediate(ToDelete[i].gameObject);
                     }
                 };
             }
             else
             {
                 Debug.Log("PrevSize " + PrevSize);
                 for (int i = PrevSize; i < targets.Length; i++)
                 {


                        // targets[i] = g.transform;

                 }
             }
             prevTargets = targets;
             PrevSize = targets.Length;
             */

        } 


        /*
        public void DeleteTarget(int toDelete)
        {
            if(toDelete > targets.Length)
            {
                Debug.Log("Can't delete target that is not on the list");
                return;
            }

            for (int i = toDelete; i < TargetHolder.transform.childCount-1; i++)
            {

                targets[i] = targets[i+1];
                targets[i].name = "Target " + i;
            }


            UnityEditor.EditorApplication.delayCall += () =>
            {
                    DestroyImmediate(TargetHolder.transform.GetChild(toDelete).gameObject);
            };



    */
        


    }
}

