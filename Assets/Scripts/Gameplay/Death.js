#pragma strict

private var life : int = 0;
private var destroyed : boolean = false;
public var duration : int = 5000;

function Start () {
	life = Time.time * 1000;
}

function Update () {
	
	if ((Time.time * 1000 - life  > duration) && !destroyed) {
		destroy();
	}
}

function destroy() {
	destroyed = true;
	var emitters : Component[] = this.gameObject.GetComponentsInChildren(ParticleEmitter, false);
	if (emitters != null) {
		for(var i = 0; i < emitters.length; i++) {
			emitters[i].particleEmitter.emit = false;
		}
	}
	var psystem : ParticleSystem = this.gameObject.GetComponentInChildren(ParticleSystem).particleSystem;
	if (psystem != null) {
		psystem.enableEmission = false;
	}
	yield new WaitForSeconds(6.0);
	Destroy(this.gameObject);
}