using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int hp = 5;

    public event System.Action<Brick> onDestroy;
    public event System.Action<Brick> onHit;

    private void OnCollisionEnter(Collision collision)
    {
        var other = collision.GetContact(0).otherCollider;

        var ball = other.GetComponent<Ball>();

        if (ball)
        {
            Debug.Log("-1hp");
            hp -= 1;
            if(this.hp <= 0)
            {
                this.onDestroy?.Invoke(this);
                Destroy(this.gameObject);
            } else
            {
                this.onHit?.Invoke(this);
            }
        }
        Debug.Log(collision.GetContact(0).otherCollider.name);
    }
    
}
