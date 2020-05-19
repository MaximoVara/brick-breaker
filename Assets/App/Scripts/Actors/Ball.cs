using UnityEngine;

public class Ball : MonoBehaviour
{
    private new Rigidbody rigidbody;
    public float startForce = 25.0F;
    private UnityEngine.Vector3 lastVelocity;
    private float lastCollisionTime;
    private void Awake()
    {
        this.rigidbody = this.GetComponent<Rigidbody>();
    }
    private void Start()
    {
        this.rigidbody.AddForce(UnityEngine.Vector3.forward * startForce, ForceMode.Force);
        this.lastVelocity = this.rigidbody.velocity;
    }
    private void FixedUpdate()
    {
        var velocity = this.rigidbody.velocity;
        var speed = velocity.magnitude;
        speed = Mathf.Clamp(speed, 15.0f, 20.0F);
        this.rigidbody.velocity = velocity.normalized * speed;
        this.lastVelocity = velocity;
    }
    private void OnCollisionEnter(Collision collision)
    {
        var velocity = this.rigidbody.velocity;
        
        var xDiff = Mathf.Abs(velocity.x = this.lastVelocity.x);
        var zDiff = Mathf.Abs(velocity.z = this.lastVelocity.z);

        if (xDiff < 0.1F || zDiff < 0.1F)
        {   
            var angle = Random.Range(-1.0F, 2.5F);
            lastVelocity = UnityEngine.Quaternion.Euler(0.0F, angle, 0.0F) * velocity;
        }
        this.rigidbody.velocity = velocity;
        
        Vector3 rando = new Vector3(0.0F, Random.Range(-1.0F, 1.0F));
        var reflection = Vector3.Reflect(this.lastVelocity.normalized, collision.GetContact(0).normal);
        this.rigidbody.velocity = reflection * velocity.magnitude + rando;
    }
}
