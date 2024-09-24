using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Manager : MonoBehaviour
{

    // Get reference to other NPC scripts for controlling the NPC
    NPC_WalkAround walkScript;
    NPC_WaypointWalk waypointScript;

    // Start is called before the first frame update
    void Start()
    {
        // set reference to script variables, throw error if not found
        if (!TryGetComponent<NPC_WalkAround>(out walkScript)) Debug.Log("Missing 'NPC_WalkAround' script reference for " + this.gameObject.name, this.gameObject);
        if (!TryGetComponent<NPC_WaypointWalk>(out waypointScript)) Debug.Log("Missing 'NPC_WaypointWalk' script reference for " + this.gameObject.name, this.gameObject);
    }

    public void SwitchToWalkAroundMovement()
    {
        walkScript.enabled = true;
        waypointScript.enabled = false;
    }

    public void SwitchToWaypointMovement()
    {
		walkScript.enabled = false;
		waypointScript.enabled = true;
	}

    public void PauseMovement()
    {
        if(!walkScript.movementPauseRequest)
        {
            walkScript.movementPauseRequest = true;
            Debug.Log("disabled npc walkaround movement");
        }
        if(!waypointScript.movementPauseRequest)
        {
			waypointScript.movementPauseRequest = true;
            Debug.Log("disabled npc waypoint movement");
        }
    }

    public void ResumeMovement()
    {
		if (walkScript.movementPauseRequest)
		{
			walkScript.movementPauseRequest = false;
			Debug.Log("enabled npc walkaround movement");
		}
		if (waypointScript.movementPauseRequest)
		{
			waypointScript.movementPauseRequest = false;
			Debug.Log("enabled npc waypoint movement");
		}
	}
}
