using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Manager : MonoBehaviour
{

    // Get reference to other NPC scripts for controlling the NPC
    NPC_WalkAround walkScript;

    // Start is called before the first frame update
    void Start()
    {
        // set reference to script variables, throw error if not found
        if (!TryGetComponent<NPC_WalkAround>(out walkScript)) Debug.Log("Missing 'NPC_WalkAround' script reference for " + this.gameObject.name, this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
