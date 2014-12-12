using UnityEngine;
using System.Collections;

public class ForcePoint : MonoBehaviour {
	
	public float pullRadius = 2;
	public float pullForce = 1;
	public bool active = false;
	
	public void FixedUpdate() {
		if (!active)
			return;
		
		foreach (Collider collider in Physics.OverlapSphere(transform.position, pullRadius)) {
			// calculate direction from target to me
			Vector3 forceDirection = transform.position - collider.transform.position;
			
			// apply force on target towards me
			if (collider.rigidbody != null)
				collider.rigidbody.AddForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime);
		}
	}

	public void OnEnable() {
		print ("force point enabled");
	}
}