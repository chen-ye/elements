using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Combo {

	public GestureMeta[] sequence;
	public GestureController gestureController;

	private int currentIndex = 0; //moves along the array as buttons are pressed
	
	public float allowedTimeBetweenGestures = 2.3f; //tweak as needed
	private float timeLastGesture;
	private List<Limb> limbList;
	
	public Combo(GestureMeta[] g, GestureController gc)
	{
		sequence = g;
		gestureController = gc;
	}
	
	//usage: call this once a frame. when the combo has been completed, it will return true
	public bool Check()
	{
		if (Time.time > timeLastGesture + allowedTimeBetweenGestures) currentIndex = 0;
		{
			if (currentIndex < sequence.Length)
			{	
				GestureMeta currentGesture = sequence[currentIndex];
				bool nextTriggered = false;
				foreach (Limb limb in currentGesture.LimbList) {
					if(gestureController.Gestures.ContainsKey(limb)) {
						//Debug.Log(limb);
						//Debug.Log(currentGesture.gesture);
						if(gestureController.Gestures[limb][currentGesture.gesture]) {
							nextTriggered = true;
							break;
						}
					}
				}

				if (nextTriggered)
				{
					timeLastGesture = Time.time;
					currentIndex++;
				}
				
				if (currentIndex >= sequence.Length)
				{
					currentIndex = 0;
					return true;
				}
				else return false;
			}
		}
		
		return false;
	}

}
