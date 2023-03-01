using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Racket : MonoBehaviour
{
    [FormerlySerializedAs("_speed")] [SerializeField]
    private float speed;

    private Vector2 _velocity;
    private Vector3 _position;

    private void FixedUpdate()
    {
        var currentPosition = transform.position;

        var deltaPositions = Vector3.Distance(currentPosition, _position);
        var direction = new Vector2(currentPosition.x - _position.x, currentPosition.y - _position.y).normalized;

        speed = deltaPositions / 0.005f;

        _velocity = direction * speed;

        _position = currentPosition;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var ball = col.gameObject.GetComponent<Ball>();
        
        var impulse = col.relativeVelocity;

        var resulImpulse = impulse + (_velocity * -1);
        ball.Rigidbody.AddForce(resulImpulse * -1 * 0.7f, ForceMode2D.Impulse);
    }
}
