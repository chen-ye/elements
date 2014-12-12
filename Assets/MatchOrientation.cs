using UnityEngine;
using System.Collections;

public class MatchOrientation : MonoBehaviour {

	public Transform target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		target.eulerAngles = transform.eulerAngles;
	}
}
