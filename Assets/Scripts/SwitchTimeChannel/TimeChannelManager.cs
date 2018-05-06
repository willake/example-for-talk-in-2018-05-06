using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChannelManager : MonoBehaviour 
{
	// Camera Reference
	public Camera mainCam;
	public Channel currentChannel;

	// Update is called once per frame
	void Update () 
	{
		// If player click J
		if(Input.GetKeyDown(KeyCode.J))
		{
			SwitchTime(Channel.Past);
		}
		
		// If player click K
		if(Input.GetKeyDown(KeyCode.K))
		{
			SwitchTime(Channel.Now);
		}
	}


	void SwitchTime(Channel targetChannel)
	{
		if(targetChannel == currentChannel)
		{
			return;
		}

		switch(targetChannel)
		{
			case Channel.Past:
			currentChannel = Channel.Past;

			// You don't need to know about this in this time
			mainCam.cullingMask |= 1 << LayerMask.NameToLayer("Past");
			mainCam.cullingMask &=  ~(1 << LayerMask.NameToLayer("Now"));
			break;
			
			case Channel.Now:
			currentChannel = Channel.Now;

			// You don't need to know about this in this time
			mainCam.cullingMask |= 1 << LayerMask.NameToLayer("Now");
			mainCam.cullingMask &=  ~(1 << LayerMask.NameToLayer("Past"));
			break;
		}
	}
}


// You should know about enum
public enum Channel
{
	Past,
	Now
}
