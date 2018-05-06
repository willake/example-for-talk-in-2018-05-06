using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
	public GameObjectPositionVariable playerPosition;
	public bool reachRightBound = true;
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

	private Vector3 velocity = Vector3.zero;
	void FixedUpdate()
	{
		switch(cameraFollowType)
		{
			case FollowType.Stick:
			transform.position = new Vector3(playerPosition.value.x,transform.position.y,transform.position.z);

			Debug.DrawLine(transform.position + new Vector3(0,StickGizmoLenth,0),transform.position + new Vector3(0,-StickGizmoLenth,0),Color.yellow);
			break;

			case FollowType.Bound:
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
			
			Debug.DrawLine(transform.position + new Vector3(boundOffsetX,boundGizmoLenth,0),transform.position + new Vector3(boundOffsetX,-boundGizmoLenth,0),Color.red);
			Debug.DrawLine(transform.position + new Vector3(-boundOffsetX,boundGizmoLenth,0),transform.position + new Vector3(-boundOffsetX,-boundGizmoLenth,0),Color.red);
			break;

			case FollowType.BoundWithSmooth:
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
		smoothSpeed = Mathf.Abs(transform.position.x + (reachRightBound? boundOffsetX:-boundOffsetX) - playerPosition.value.x) * 0.002f;
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
