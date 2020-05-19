using UnityEngine;

public class Ball : MonoBehaviour {
	public float startForce = 15.0F;

	private new Rigidbody rigidbody;
	private Vector3 lastVelocity;

	private void Awake() {
		this.rigidbody = this.GetComponent<Rigidbody>();
	}

	private void Start() {
		this.rigidbody.AddForce(Vector3.forward * this.startForce, ForceMode.Force);
		this.lastVelocity = this.rigidbody.velocity;
	}

	private void FixedUpdate() {
		var velocity = this.rigidbody.velocity;
		var speed = velocity.magnitude;

		speed = Mathf.Clamp(speed, 15.0F, 20.0F);

		this.rigidbody.velocity = velocity.normalized * speed;

		this.lastVelocity = velocity;
	}

	private void OnCollisionEnter(Collision collision) {
		var velocity = this.rigidbody.velocity;

		var xDiff = Mathf.Abs(velocity.x - this.lastVelocity.x);
		var zDiff = Mathf.Abs(velocity.z - this.lastVelocity.z);

		// possible stuck phase...
		if(xDiff < 0.1F || zDiff < 0.1F) {
			var angle = Random.Range(0.01F, 1.5F);

			lastVelocity = Quaternion.Euler(0.0F, angle, 0.0F) * lastVelocity;
		}

		// 100% ellastic collision...
		var reflection = Vector3.Reflect(this.lastVelocity.normalized, collision.GetContact(0).normal);

		this.rigidbody.velocity = reflection * velocity.magnitude;
	}
}
