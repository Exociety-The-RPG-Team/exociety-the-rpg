using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class NPC_WaypointWalk : MonoBehaviour
{
    // Waypoint vars
    public GameObject waypointContainer;
    private GameObject[] waypointsGO;
    private waypointDataContainer[] waypointsData;
    private int curWaypointIndex = 0;
    private GameObject curWaypointGO;
    private float curDistance = 0.0f;
    private float minDistance = 0.03f;

    // Movement vars
    public float moveSpeed = 0.8f;
    public bool movementPauseRequest = false;
    private bool loopComplete = false;  // var to control progressing through each waypoint
    [SerializeField]
    private bool keepLooping = false;   // var to control if npc paths to first waypoint after reaching last waypoint


    // Start is called before the first frame update
    void Start()
    {
		// Initialize vars
		waypointsData = waypointContainer.GetComponentsInChildren<waypointDataContainer>();
        //Debug.Log(waypointsData.Length.ToString());
        
        waypointsGO = new GameObject[waypointsData.Length];

        
        // Populate waypoints Gameobject array
        if (waypointContainer == null || waypointsData.Length < 1) Debug.Log("Missing waypoints for " + this.gameObject.name, this.gameObject);

        foreach (waypointDataContainer waypointInfo in waypointsData)
        {
            waypointsGO[waypointInfo.waypointNumber] = waypointInfo.gameObject;
        }

        curWaypointGO = waypointsGO[curWaypointIndex];
        
	}

    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!loopComplete && !movementPauseRequest)
        {
            curDistance = math.sqrt(Mathf.Pow(curWaypointGO.transform.position.x - this.transform.position.x, 2) + Mathf.Pow(curWaypointGO.transform.position.y - this.transform.position.y, 2));
			if (curDistance > minDistance) 
            {
                this.transform.position += (curWaypointGO.transform.position - this.transform.position).normalized * moveSpeed * Time.deltaTime;
            }
            else if (curWaypointIndex < waypointsGO.Length - 1) 
            {
				curWaypointIndex += 1;
				//Debug.Log("Targeting next waypoint, index " + curWaypointIndex);
				curWaypointGO = waypointsGO[curWaypointIndex];
			}
            else if ((curWaypointIndex >= waypointsGO.Length - 1) && keepLooping)
            {
				//Debug.Log("Restarting loop");
				curWaypointIndex = 0;
				curWaypointGO = waypointsGO[curWaypointIndex];
			}
            else
            {
				//Debug.Log("Stopping loop");
				loopComplete = true;
            }
            
        }
    }
    

}
