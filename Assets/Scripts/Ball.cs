using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private Rigidbody2D _rigidbody;

    public float Speed => _speed;
    public Rigidbody2D Rigidbody => _rigidbody;
    
    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var velocity = _rigidbody.velocity;
        _speed = velocity.magnitude;
    }
}
