using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
	// Player Position Reference
	public GameObjectPositionVariable playerPosition;
	
	// If player reach the bound of camera, if player reach right side than set true, else set false (left side)
	public bool reachRightBound = true;

	// Camera follow type : Stick, Bound, Smooth
	public FollowType cameraFollowType;

	[HeaderAttribute("Stick Mode")]
	public float StickGizmoLenth;

	[HeaderAttribute("Bound Mode")]
	public float boundOffsetX;
	public float boundGizmoLenth;

	[HeaderAttribute("Smooth Mode")]
	public float smoothOffsetX;
	public float smoothGizmoLenth;
	public float smoothSpeed;
	
	void FixedUpdate()
	{
		// You should know about switch!
		switch(cameraFollowType)
		{
			case FollowType.Stick:
			// Camera position stick with player
			transform.position = new Vector3(playerPosition.value.x,transform.position.y,transform.position.z);

			// Draw the yellow line for you to see in Unity
			Debug.DrawLine(transform.position + new Vector3(0,StickGizmoLenth,0),transform.position + new Vector3(0,-StickGizmoLenth,0),Color.yellow);
			break;

			case FollowType.Bound:
			// If player reach the bounds than follow player immediately
			if(playerPosition.value.x < transform.position.x - boundOffsetX)
			{
				reachRightBound = true;
				FollowWithBound();
			}
			else if(playerPosition.value.x > transform.position.x + boundOffsetX)
			{
				reachRightBound = false;
				FollowWithBound();
			}
			
			// Draw the red line for you to see in Unity
			Debug.DrawLine(transform.position + new Vector3(boundOffsetX,boundGizmoLenth,0),transform.position + new Vector3(boundOffsetX,-boundGizmoLenth,0),Color.red);
			Debug.DrawLine(transform.position + new Vector3(-boundOffsetX,boundGizmoLenth,0),transform.position + new Vector3(-boundOffsetX,-boundGizmoLenth,0),Color.red);
			break;

			case FollowType.BoundWithSmooth:

			// If player reach the bounds than follow player smoothly
			if(playerPosition.value.x < transform.position.x - boundOffsetX)
			{
				reachRightBound = true;
				FollowWithSmooth();
			}
			else if(playerPosition.value.x > transform.position.x + boundOffsetX)
			{
				reachRightBound = false;
				FollowWithSmooth();
			}

			// Draw the red and yellow line for you to see in Unity
			Debug.DrawLine(transform.position + new Vector3(boundOffsetX,boundGizmoLenth,0),transform.position + new Vector3(boundOffsetX,-boundGizmoLenth,0),Color.red);
			Debug.DrawLine(transform.position + new Vector3(-boundOffsetX,boundGizmoLenth,0),transform.position + new Vector3(-boundOffsetX,-boundGizmoLenth,0),Color.red);
			Debug.DrawLine(transform.position + new Vector3(smoothOffsetX,smoothGizmoLenth,0),transform.position + new Vector3(smoothOffsetX,-smoothGizmoLenth,0),Color.yellow);
			Debug.DrawLine(transform.position + new Vector3(-smoothOffsetX,smoothGizmoLenth,0),transform.position + new Vector3(-smoothOffsetX,-smoothGizmoLenth,0),Color.yellow);
			break;
		}
	}

	void FollowWithBound()
	{
		transform.position = new Vector3(playerPosition.value.x + (reachRightBound? boundOffsetX:-boundOffsetX),transform.position.y,transform.position.z);
	}

	void FollowWithSmooth()
	{
		// Caculate follow speed with distance
		smoothSpeed = Mathf.Abs(transform.position.x + (reachRightBound? boundOffsetX:-boundOffsetX) - playerPosition.value.x) * 0.002f;

		// You just need to know it make camera follow smoothly
		float smoothX = Vector3.Lerp(transform.position,new Vector3(playerPosition.value.x + (reachRightBound? boundOffsetX:-boundOffsetX),transform.position.y,transform.position.z),smoothSpeed).x;
		transform.position = new Vector3(smoothX,transform.position.y,transform.position.z);
	}
}

public enum FollowType 
{
	Stick,
	Bound,
	BoundWithSmooth
}
