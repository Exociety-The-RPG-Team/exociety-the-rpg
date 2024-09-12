using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressEtest : MonoBehaviour
{
    public GameObject[] npcs;
    public List<NPC_WalkAround> npcsWalkScripts = new List<NPC_WalkAround>();

    // Start is called before the first frame update
    void Start()
    {
        npcs = GameObject.FindGameObjectsWithTag("NPC");
        if (npcs != null )
        {
            foreach (GameObject npc in npcs)
            {
                npcsWalkScripts.Add(npc.GetComponent<NPC_WalkAround>());
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            foreach (NPC_WalkAround walkAround in npcsWalkScripts)
            {
                walkAround.pauseMovement = true;
            }
        }

		if (Input.GetKeyDown(KeyCode.Q))
		{
			foreach (NPC_WalkAround walkAround in npcsWalkScripts)
			{
				walkAround.pauseMovement = false;
			}
		}
	}
}
