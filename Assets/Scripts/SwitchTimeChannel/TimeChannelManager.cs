using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChannelManager : MonoBehaviour 
{
	
	public Camera mainCam;
	public Channel currentChannel;
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.J))
		{
			SwitchTime(Channel.Past);
		}
		
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
			mainCam.cullingMask |= 1 << LayerMask.NameToLayer("Past");
			mainCam.cullingMask &=  ~(1 << LayerMask.NameToLayer("Now"));
			break;
			
			case Channel.Now:
			currentChannel = Channel.Now;
			mainCam.cullingMask |= 1 << LayerMask.NameToLayer("Now");
			mainCam.cullingMask &=  ~(1 << LayerMask.NameToLayer("Past"));
			break;
		}
	}
}

public enum Channel
{
	Past,
	Now
}
