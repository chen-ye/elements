using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Limb {
	LeftArm,
	RightArm,
	LeftLeg,
	RightLeg
}

public enum Gesture {
	Fist,
	FingersSpread,
	WaveIn,
	WaveOut,
	DoubleTap,
	None //Special case.  Don't use this in the Gesture Dictionary
}

public struct GestureMeta {
	public List<Limb> LimbList;
	public Gesture gesture;
	public GestureMeta(List<Limb> LimbList, Gesture gesture) {
		this.LimbList = LimbList;
		this.gesture = gesture;
	}
}

public class GestureController : MonoBehaviour {
	public delegate void LowLevelGesture();
	public event LowLevelGesture OnLLGesture;

	public delegate void HighLevelGesture();
	public event HighLevelGesture OnHLGesture;

	public Dictionary<Limb,Dictionary<Gesture,bool>> Gestures = new Dictionary<Limb,Dictionary<Gesture,bool>>();

	public GestureController() {
		//Move these into their respective controller awakes
		Gestures [Limb.LeftArm] = new Dictionary<Gesture,bool>();
		Gestures [Limb.LeftLeg] = new Dictionary<Gesture,bool>();
		Gestures [Limb.RightArm] = new Dictionary<Gesture,bool>();
		Gestures [Limb.RightLeg] = new Dictionary<Gesture,bool>();
	}

	public void fireLLGesture() {
		if(OnLLGesture != null)
		{
			OnLLGesture();
		}
	}

	public void fireHLGesture() {
		if(OnHLGesture != null)
		{
			OnHLGesture();
		}
	}
}