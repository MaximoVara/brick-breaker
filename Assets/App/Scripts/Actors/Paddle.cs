using UnityEngine;

public class Paddle : MonoBehaviour {
	public float speed = 5.0F;

	private Rigidbody rigidbody;
	private float xAxis = 0.0F;

	private void Awake() {
		this.rigidbody = this.GetComponent<Rigidbody>();
	}

	private void FixedUpdate() {
		var velocity = this.rigidbody.velocity;

		velocity.x = this.speed * this.xAxis;

		this.rigidbody.velocity = velocity;
	}

	private void Update() {
		this.xAxis = Input.GetAxis("Horizontal");
	}
}