using UnityEngine;
using System.Collections;

public class OVRDetect : MonoBehaviour {

	public GameObject nonOVRCam;
	public GameObject ovrRig;
	private bool oculusActive = true;
	
	void Update () {
		if (oculusActive && !OVRManager.display.isPresent){
			ovrRig.SetActive(false);
			nonOVRCam.SetActive(true);
		}
		if (!oculusActive && OVRManager.display.isPresent){
			nonOVRCam.SetActive(false);
			ovrRig.SetActive(true);
		}
		oculusActive = OVRManager.display.isPresent;
	}
}
