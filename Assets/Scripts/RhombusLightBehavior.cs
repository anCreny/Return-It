using UnityEngine;

public class RhombusLightBehavior : MonoBehaviour
{

    private ParticleSystem _particleSystem;
    [SerializeField] private RhombusTarget target;
    
    private void Awake()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    public void ReduceLight()
    {
        _particleSystem.startSize = 1.5f;
        _particleSystem.Clear();
        _particleSystem.Play();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        target.SetLastBallContact(col);
        var ball = col.gameObject.GetComponent<Ball>();
        
        var impulse = col.relativeVelocity;
        ball.Rigidbody.AddForce(impulse * -1 * 0.7f, ForceMode2D.Impulse);
        target.ReduceDurability();
    }

    public void TurnOffLight()
    {
        _particleSystem.Clear();
        Destroy(GetComponent<PolygonCollider2D>());
    }
}
