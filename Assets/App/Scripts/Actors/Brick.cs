using UnityEngine;

public class Brick : MonoBehaviour {
	public int hp = 1;
	public event System.Action<Brick> onHit;
	public event System.Action<Brick> onDestroyed;

	private void OnCollisionEnter(Collision collision) {
		var other = collision.GetContact(0).otherCollider;

		var ball = other.GetComponent<Ball>();

		if(ball != null) {
			this.hp--;

			if(this.hp <= 0) {
				// we raise the event...
				this.onDestroyed?.Invoke(this);

				Destroy(this.gameObject);
			} else {
				this.onHit?.Invoke(this);
			}
		}
	}
}
