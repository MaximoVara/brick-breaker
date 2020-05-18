using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 5.0F;
    private Rigidbody rigidBody;
    private float xAxis = 0.0F;
    
    private void Awake(){
        this.rigidBody = this.GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.xAxis = Input.GetAxis("Horizontal");   
    }
    private void FixedUpdate() {
        //var position = this.rigidBody.position;
        //position.x += this.xAxis * Time.deltaTime * speed;
        //this.rigidBody.position = position;
        var velocity = rigidBody.velocity;
        velocity.x = this.speed * this.xAxis;
        this.rigidBody.velocity = velocity;
    }
}
