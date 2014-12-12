using UnityEngine;
using System.Collections;

using Pose = Thalmic.Myo.Pose;

public class FireElement : MonoBehaviour {

	// Myo game object to connect with.
	// This object must have a ThalmicMyo script attached.
	public GameObject myo = null;

	public Rigidbody airProjectile;
	public Rigidbody fireProjectile;
	public Rigidbody earthProjectile;
	public Rigidbody waterProjectile;

	public CharacterController trust;
	
	private Rigidbody projectile;
	private int activeElement = 2;
	
	public float airVelocity = 200;
	public float fireVelocity = 100;
	public float earthVelocity = 50;
	public float waterVelocity = 40;
	
	private bool rock = false;
	private bool air = false;
	private bool water = false;

	private ThalmicMyo thalmicMyo;
	private bool fireMyo = false;
	private bool drawMyo = false;

	// The pose from the last update. This is used to determine if the pose has changed
	// so that actions are only performed upon making them rather than every frame during
	// which they are active.
	private Pose _lastPose = Pose.Unknown;

	// Use this for initialization
	void Start () {
		thalmicMyo = myo.GetComponent<ThalmicMyo> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (thalmicMyo.pose != _lastPose) {
			_lastPose = thalmicMyo.pose;
			print (thalmicMyo.name + " pose: " + thalmicMyo.pose);
			
			if (thalmicMyo.pose == Pose.FingersSpread || thalmicMyo.pose == Pose.WaveIn) {
				drawMyo = true;
			}
			if (thalmicMyo.pose == Pose.Fist) {
				fireMyo = true;
			}
		}

		if (Input.GetKeyDown("1")) {
			activeElement = 1;
		}
		if (Input.GetKeyDown("2")) {
			activeElement = 2;
		}
		if (Input.GetKeyDown("3")) {
			activeElement = 3;
		}
		if (Input.GetKeyDown("4")) {
			activeElement = 4;
		}

		if (Input.GetMouseButtonDown(0) || fireMyo) {
			fireMyo = false;
			switch(activeElement) {
			case 1:
				fireAir();
				break;
			case 2:
				fireFire();
				break;
			case 3:
				fireEarth();
				break;
			case 4:
				fireWater();
				break;
			}
		}
		if (Input.GetMouseButtonDown(1) || drawMyo) {
			drawMyo = false;
			switch(activeElement) {
			case 1:
				//chargeAir();
				break;
			case 2:
				//chargeFire();
				break;
			case 3:
				chargeEarth();
				break;
			case 4:
				chargeWater();
				break;
			}
		}


	}

	void fireAir() {
		Rigidbody clone;
		clone = (Rigidbody) Instantiate(airProjectile, transform.position + transform.TransformDirection(Vector3.forward * (float) 0.5), transform.rotation);
		clone.AddForce(transform.TransformDirection (Vector3.forward * airVelocity * 500000));
		//trust.AddImpact (transform.TransformDirection (Vector3.back * airVelocity * 50000));
	}

	void fireFire() {
		Rigidbody clone;
		clone = (Rigidbody) Instantiate(fireProjectile, transform.position + transform.TransformDirection(Vector3.forward * (float) 0.5), transform.rotation);
		clone.velocity = transform.TransformDirection (Vector3.forward * fireVelocity);
	}
	
	void fireEarth() {
		if (rock) {
			projectile.AddForce (transform.TransformDirection(Vector3.forward * earthVelocity * 100000));
			rock = false;
		}
	}
	
	void chargeEarth() {
		rock = true;
		projectile = (Rigidbody) Instantiate(earthProjectile, transform.position + transform.TransformDirection((Vector3.forward * 4) + (Vector3.down * 2)), transform.rotation);
		//this.gameObject.Find("ForcePoint").SetActive(true);
		projectile.AddForce (Vector3.up * earthVelocity * 5000);
	}
	
	void fireWater() {
		if (water) {
			projectile.AddForce (transform.TransformDirection(Vector3.forward * waterVelocity * 100));
		}
	}
	
	void chargeWater() {
		water = true;
		projectile = (Rigidbody) Instantiate(waterProjectile, transform.position + transform.TransformDirection((Vector3.forward * 4) + (Vector3.down * 1)), transform.rotation);
		//projectile.collider.active = false;
		projectile.AddForce (Vector3.up * waterVelocity * 50);
		//yield new WaitForSeconds(0.5);
		//projectile.collider.active = true;
	}




}

