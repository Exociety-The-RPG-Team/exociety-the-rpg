using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private float playerSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // moved movement code to fixed update to fix jittering issue with rigidbody
	private void FixedUpdate()
	{
		float horInput = Input.GetAxis("Horizontal");
		float vertInput = Input.GetAxis("Vertical");
		Vector3 direction = new Vector3(horInput, vertInput, 0);
		this.transform.Translate(direction * playerSpeed * Time.deltaTime);
	}
}
