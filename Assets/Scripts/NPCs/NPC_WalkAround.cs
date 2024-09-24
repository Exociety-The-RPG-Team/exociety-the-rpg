using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Components required due to collision detection method
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class NPC_WalkAround : MonoBehaviour
{
	// The transform of the attached gameobject to be moved
	internal Transform thisTransform;

	// The movement speed of the object
	public float moveSpeed = 0.5f;

	// An option for otherscripts to interrupt the movement - should be when interacting with NPCs
	[NonSerialized]
	public bool movementPauseRequest = false;

	// Internal var mainly used for controlling collision responses and other internal algorithms. Should be prioritized over movementPauseRequest where sensible.
	private bool movementCommand = true;

	// A minimum and maximum time delay for taking a decision, choosing a direction to move in
	public (int min, int max) decisionTime = (1, 4);
	private float decisionTimeCount = 0;

	// The possible directions that the object can move int, right, left, up, down, and zero for staying in place.
	private List<Vector2> moveDirections = new List<Vector2> { Vector2.right, Vector2.left, Vector2.up, Vector2.down, Vector2.zero };
	private Vector2 currentMoveDirection = Vector2.zero;
	private Vector2 lastMoveDirection = Vector2.zero;

	// A struct to make modifying available directions easily done from the editor
	[Serializable]
	public struct AvailableDirection
	{
		public bool up;
		public bool down;
		public bool left;
		public bool right;
	}
	[SerializeField]
	private AvailableDirection blockedDirections;



	// Use this for initialization
	void Start()
	{
		// Cache the transform for quicker access
		thisTransform = this.transform;

		// Set a random time delay for taking a decision ( changing direction, or standing in place for a while )
		decisionTimeCount = UnityEngine.Random.Range(decisionTime.min, decisionTime.max);

		// Remove unwanted possible movement directions
		if (blockedDirections.up)
		{
			moveDirections.Remove(Vector2.up);
		}
		if (blockedDirections.down)
		{
			moveDirections.Remove(Vector2.down);
		}
		if (blockedDirections.left)
		{
			moveDirections.Remove(Vector2.left);
		}
		if (blockedDirections.right)
		{
			moveDirections.Remove(Vector2.right);
		}

		// Choose a movement direction, or stay in place
		ChooseMoveDirection();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		// Move the NPC in random direction.
		if (!movementPauseRequest && movementCommand)
		{
			thisTransform.position = new Vector2(thisTransform.position.x + (moveSpeed * Time.deltaTime * currentMoveDirection.x), thisTransform.position.y + (moveSpeed * Time.deltaTime * currentMoveDirection.y));
		

			// When decision timer is up, pick a new direction and walk duration
			if (decisionTimeCount > 0) decisionTimeCount -= Time.deltaTime;
			else
			{
				// Choose a random time delay for making a movement decision ( how long to walk or stand in place )
				decisionTimeCount = UnityEngine.Random.Range(decisionTime.min, decisionTime.max);

				// Choose a movement direction, or stay in place
				ChooseMoveDirection();
			}
		}
	}

	// Make a randome choice about which direction to move.
	// Will alternate stopping and moving.
	void ChooseMoveDirection()
	{
		if (currentMoveDirection == Vector2.zero)
		{
			currentMoveDirection = moveDirections[Mathf.FloorToInt(UnityEngine.Random.Range(0, moveDirections.Count))];
		}
		else
		{
			currentMoveDirection = Vector2.zero;
		}
		
	}

	// Basic collision detection, the responce is to set the current direction opposite of the collision. 
	// Assumes a box 2d collider is used. 
	private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
	{

		if (!collision.gameObject.tag.Equals("Player"))
		{
			int colDirection = DetermineRectCollisionSide(collision);
			switch (colDirection)
			{
				case 0:
					// Top side collision
					currentMoveDirection = Vector2.down;
					break;
				case 1:
					// Right side collision
					currentMoveDirection = Vector2.left;
					break;
				case 2:
					// Bottom side collision
					currentMoveDirection = Vector2.up;
					break;
				case 3:
					// Left side collision
					currentMoveDirection = Vector2.right;
					break;
				default:
					break;
			}

			decisionTimeCount = UnityEngine.Random.Range(decisionTime.min, decisionTime.max);
		}
	}
	
	private void OnCollisionStay2D(UnityEngine.Collision2D collision)
	{
		if (collision.gameObject.tag.Equals("Player"))
		{
			movementCommand = false;
		}
	}

	
	private void OnCollisionExit2D(UnityEngine.Collision2D collision)
	{
		if (collision.gameObject.tag.Equals("Player"))
		{
			movementCommand = true;
			decisionTimeCount = decisionTime.min;
			currentMoveDirection = Vector2.zero;
		}
		
	}

	/* Given the collision data for a rect collider this method returns an int representation of which side the collision occured on. 
	 * int to side reference:
	 * 0 = North / Top
	 * 1 = East / Right
	 * 2 = South / Bottom
	 * 3 = West / Left
	 * 
	 * -1 = something didn't work
	 */
	private int DetermineRectCollisionSide(UnityEngine.Collision2D collision)
	{
		int colSide = -1;

		// Get collision vertices
		ContactPoint2D[] contactPoints = new ContactPoint2D[4];
		collision.GetContacts(contactPoints);
		float v1_x = contactPoints[0].point.x;
		float v1_y = contactPoints[0].point.y;
		float v2_x = contactPoints[1].point.x;
		float v2_y = contactPoints[1].point.y;

		// Use collision vertices to determin which side of npc is colliding
		if (v1_y == v2_y)
		{
			if (v1_y > thisTransform.position.y)
			{
				// Top side collision
				colSide = 0;
			}
			else
			{
				// Bottom side collision
				colSide = 2;
			}
		}
		else
		{
			if (v1_x > thisTransform.position.x)
			{
				// Right side collision
				colSide = 1;
			}
			else
			{
				// Left side collision
				colSide = 3;
			}
		}

		return colSide;
	}
}
