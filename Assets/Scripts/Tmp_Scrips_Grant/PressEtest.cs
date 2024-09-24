using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressEtest : MonoBehaviour
{
    public GameObject[] npcs;
    public List<NPC_Manager> npcsManagerScripts = new List<NPC_Manager>();

    // Start is called before the first frame update
    void Start()
    {
        npcs = GameObject.FindGameObjectsWithTag("NPC");
        if (npcs != null )
        {
            foreach (GameObject npc in npcs)
            {
                npcsManagerScripts.Add(npc.GetComponent<NPC_Manager>());
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            foreach (NPC_Manager manager in npcsManagerScripts)
            {
                manager.pauseNPCMovement();
            }
        }

		if (Input.GetKeyDown(KeyCode.Q))
		{
			foreach (NPC_Manager manager in npcsManagerScripts)
			{
                manager.resumeNPCMovement();
			}
		}
	}
}
