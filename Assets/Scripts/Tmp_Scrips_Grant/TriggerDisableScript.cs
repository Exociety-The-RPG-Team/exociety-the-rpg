using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDisableScript : MonoBehaviour
{
    public PressEtest pressEScript;

	public void Start()
	{
		pressEScript.enabled = false;
	}

	private void OnTriggerStay2D(UnityEngine.Collider2D collision)
	{
		Debug.Log("trigger staying");
		pressEScript.enabled = true;
	}

	private void OnTriggerExit2D(UnityEngine.Collider2D collision)
	{
		Debug.Log("Trigger exit");
		pressEScript.enabled = false;
	}
}
